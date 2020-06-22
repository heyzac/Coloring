using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

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

    //변수 선언
    public int mapHeight;
    public int mapWeight;

    private void Awake()
    {
        puzzleIn = GameObject.Find("ScriptObject").GetComponent<PuzzleIn>();
        argumentedCellSize = GetComponent<Grid>().cellSize.x;
    }

    private void Start()
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
            if (objectList[i].GetComponent<Object>().GetPosition() == v3)
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
            if (tileList[i].GetComponent<Object>().GetPosition() == v3)
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
        AssetDatabase.Refresh();
    }

    public void LoadLevel()
    {
        if(levelData == null)
        {
            Debug.LogWarning("Cannot Find Level's Data");
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
        GameObject tileListObject;
        if (obj.objectType <= PuzzleIn.OBJECT_AND_TILE_BOUNDARY)
        {
            tileListObject = GameObject.Find("TileList");
        }
        else
        {
            tileListObject = GameObject.Find("ObjectList");
        }

        GameObject instance = Instantiate(puzzleIn.gameElement[obj.objectType], tileListObject.transform);

        instance.GetComponent<Object>().x = obj.x;
        instance.GetComponent<Object>().y = obj.y;
        instance.GetComponent<Object>().rotate = obj.rotate;
        instance.GetComponent<Object>().objectType = obj.objectType;

        return instance.GetComponent<Object>();
    }

    private void TileArrayment()
    {
        TileObject tileObject;
        for (int i = 0; i < tileList.Count; i++)
        {
            tileObject = tileList[i].GetComponent<TileObject>();
        }
    }
    private void ObjectArrayment()  
    {
        PuzzleObject puzzleObject;
        for (int i = 0; i < objectList.Count; i++)
        {
            puzzleObject = objectList[i].GetComponent<PuzzleObject>();
        }
    }

    public int RotateOverflow(int rotate)
    {
        if (IsAllowRotate(rotate))
            return rotate;

        else if (rotate < 0)
            for (; !IsAllowRotate(rotate); rotate += 4) ;

        else if (rotate > 3)
            for (; !IsAllowRotate(rotate); rotate -= 4) ;

        return rotate;
    }

    private bool IsAllowRotate(int rotate)
    {
        return (rotate >= 0 && rotate <= 3);
    }
}
