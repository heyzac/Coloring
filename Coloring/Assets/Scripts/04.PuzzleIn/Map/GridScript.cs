using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    //참조 목적의 수많은(예정) 변수들
    public int currentStage;
    public int currentLevel;
    public float argumentedCellSize;

    //클래스 선언
    public MapData levelData;
    private PuzzleIn puzzleIn;

    //게임 오브젝트
    public List<GameObject> elementList = new List<GameObject>();
    public List<GameObject> objectList = new List<GameObject>();
    public List<GameObject> tileList = new List<GameObject>();
    //public Dictionary<GameObject, Vector3Int> tileMapArray = new Dictionary<GameObject, Vector3Int>();
    //public Dictionary<GameObject, Vector3Int> objectMapArray = new Dictionary<GameObject, Vector3Int>();

    //변수 선언
    public int mapHeight;
    public int mapWeight;

    void Awake()
    {
        puzzleIn = GameObject.Find("ScriptObject").GetComponent<PuzzleIn>();
        argumentedCellSize = GetComponent<Grid>().cellSize.x;
    }

    void Start()
    {
        levelData = ScriptableObject.CreateInstance<MapData>();
        currentLevel = 1;
        levelData = (MapData)AssetDatabase.LoadAssetAtPath("Assets/Resources/Levels/Level" + currentLevel + ".asset", typeof(MapData));
        //LoadLevel();
    }

    public GameObject GetObjectWithPosition(Vector3Int v3)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].GetComponent<Object>().pos == v3)
            {
                return objectList[i];
            }
        }
        return null;
    }

    public GameObject GetTileWithPosition(Vector3Int v3)
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            if (tileList[i].GetComponent<Object>().pos == v3)
            {
                return tileList[i];
            }
        }
        return null;
    }

    public void GetMapMaximumSize()
    {
        int tempPos;
        int tempMax = 0;
        int tempMin = 0;

        for (int i = 0; i < elementList.Count; i++)
        {
            tempPos = elementList[i].GetComponent<Object>().x;

            if (tempPos > tempMax)
                tempMax = tempPos;
            else if (tempPos < tempMin)
                tempMin = tempPos;
        }
        mapWeight = (Mathf.Abs(tempMax) + Mathf.Abs(tempMin) + 1);

        for (int i = 0; i < elementList.Count; i++)
        {
            tempPos = elementList[i].GetComponent<Object>().y;

            if (tempPos > tempMax)
                tempMax = tempPos;
            else if (tempPos < tempMin)
                tempMin = tempPos;
        }
        mapHeight = (Mathf.Abs(tempMax) + Mathf.Abs(tempMin) + 1);
    }

    public void saveasdf()
    {
        string filePath = Application.dataPath + "/Resources/Levels";

        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        Test lafa = ScriptableObject.CreateInstance<Test>();
        lafa.lst = new List<int>();
        lafa.lst.Add(1);
        lafa.lst.Add(2);
        lafa.lst.Add(3);
        lafa.lst.Add(5);

        string fileName = string.Format("Assets/Resources/Levels/Level{1}.asset", filePath, name);

        AssetDatabase.CreateAsset(lafa, fileName);
        AssetDatabase.SaveAssets();
    }

    public void SaveLevel()
    {
        CreateAsset();
    }

    private void CreateAsset()
    {
        string filePath = Application.dataPath + "/Resources/Levels";

        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        MapData map = ScriptableObject.CreateInstance<MapData>();
        map.elements = new List<LoadingObject>();
        map.tiles = new List<LoadingObject>();
        map.objects = new List<LoadingObject>();

        Object obj;
        for (int i = 0; i < tileList.Count; i++)
        {
            obj = tileList[i].GetComponent<Object>();
            map.tiles.Add(new LoadingObject(obj.x, obj.y, obj.rotate, obj.objectType));
            map.elements.Add(new LoadingObject(obj.x, obj.y, obj.rotate, obj.objectType));
        }

        for (int i = 0; i < objectList.Count; i++)
        {
            obj = objectList[i].GetComponent<Object>();
            map.objects.Add(new LoadingObject(obj.x, obj.y, obj.rotate, obj.objectType));
            map.elements.Add(new LoadingObject(obj.x, obj.y, obj.rotate, obj.objectType));
        }

        string fileName = string.Format("Assets/Resources/Levels/Level{1}.asset", filePath, name);

        AssetDatabase.CreateAsset(map, fileName);
        AssetDatabase.SaveAssets();

        Debug.Log("Save Complete! Level's Element List Count : " + map.elements.Count);
    }
    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
 
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        //AssetDatabase.Refresh();
    }

    public void LoadLevel()
    {
        if(levelData == null)
        {
            Debug.Log("Cannot Find Level's Data");
            return;
        }
        ObjectLoading();
    }

    private void ObjectLoading()
    {
        for (int i = elementList.Count - 1; i >= 0; i--)
        {
            Destroy(elementList[i]);
        }

        elementList.Clear();
        tileList.Clear();
        objectList.Clear();

        foreach (LoadingObject v in levelData.tiles)
        {
            Object obj = ObjectCreate(v);
            obj.First();
            tileList.Add(obj.gameObject);
            elementList.Add(obj.gameObject);
        }

        foreach (LoadingObject v in levelData.objects)
        {
            Object obj = ObjectCreate(v);
            obj.First();
            objectList.Add(obj.gameObject);
            elementList.Add(obj.gameObject);
        }

        GetMapMaximumSize();
        //TileArrayment();
        //ObjectArrayment();

        Debug.Log("Load Complete! Element List's Count : " + elementList.Count);
    }
    public Object ObjectCreate(LoadingObject obj)
    {
        GameObject tileListObject = GameObject.Find("TileList");
        GameObject instance = Instantiate(puzzleIn.gameElement[obj.objectType], tileListObject.transform) as GameObject;
        instance.GetComponent<Object>().x = obj.x;
        instance.GetComponent<Object>().y = obj.y;
        instance.GetComponent<Object>().rotate = obj.rotate;
        instance.GetComponent<Object>().objectType = obj.objectType;
        return instance.GetComponent<Object>();
    }

    private void TileArrayment()
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            TileObject tileObject = tileList[i].GetComponent<TileObject>();
            //tileMapArray.Add(tileList[i], new Vector3Int(tileObject.x + 6, tileObject.y + 4, 0));
        }
    }
    private void ObjectArrayment()  
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            PuzzleObject puzzleObject = objectList[i].GetComponent<PuzzleObject>();
            //objectMapArray.Add(objectList[i], new Vector3Int(puzzleObject.x + 6, puzzleObject.y + 4, 0));
        }
    }
}
