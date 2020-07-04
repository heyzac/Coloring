using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //참조 목적의 수많은(예정) 변수들
    public float argumentedCellSize;

    //클래스 선언
    public MapData levelData;
    private PuzzleIn puzzleIn;

    //변수 선언
    public int mapHeight;
    public int mapWeight;
    public int completeRequires;
    public int inputerOperating;

    //게임 오브젝트
    public List<GameObject> elementList = new List<GameObject>();
    public List<GameObject> objectList = new List<GameObject>();
    public List<GameObject> tileList = new List<GameObject>();

    private void Awake()
    {
        EventInitialization();
        puzzleIn = GameObject.Find("ScriptObject").GetComponent<PuzzleIn>();
        argumentedCellSize = GetComponent<Grid>().cellSize.x;
    }
    private void Start()
    {
        levelData = ScriptableObject.CreateInstance<MapData>();
        string loadPath = "Assets/Resources/Levels/Level " + (PuzzleIn.currentStage + 1) + "-" + (PuzzleIn.currentLevel + 1) + ".asset";
        LoadLevel(PuzzleIn.currentLevel, PuzzleIn.currentStage);
    }

    private void EventInitialization()
    {
        PuzzleIn.EventLineBegin += InputerOperationCountInit;
    }
    private void InputerOperationCountInit()
    {
        inputerOperating = 0;
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
            obj = tileList[i].GetComponent<TileObject>();

            switch (obj.objectType)
            {
                case 0:
                    LoadingEmpty empty = new LoadingEmpty(obj as EmptyTile);
                    empty.Init(obj.x, obj.y, obj.rotate, obj.objectType);
                    map.tiles.Add(empty);
                    map.elements.Add(empty);
                    break;
                case 1:
                    LoadingWall wall = new LoadingWall(obj as WallTile);
                    wall.Init(obj.x, obj.y, obj.rotate, obj.objectType);
                    map.tiles.Add(wall);
                    map.elements.Add(wall);
                    break;
                case 2:
                    LoadingOutputer outputer = new LoadingOutputer(obj as Outputer);
                    outputer.Init(obj.x, obj.y, obj.rotate, obj.objectType);
                    map.tiles.Add(outputer);
                    map.elements.Add(outputer);
                    break;
                case 3:
                    LoadingInputer inputer = new LoadingInputer(obj as Inputer);
                    inputer.Init(obj.x, obj.y, obj.rotate, obj.objectType);
                    map.tiles.Add(inputer);
                    map.elements.Add(inputer);
                    break;
            }
        }
        for (int i = 0; i < objectList.Count; i++)
        {
            obj = objectList[i].GetComponent<PuzzleObject>();

            switch (obj.objectType)
            {
                case 4:
                    LoadingReflector reflector = new LoadingReflector(obj as Reflector);
                    reflector.Init(obj.x, obj.y, obj.rotate, obj.objectType);
                    map.objects.Add(reflector);
                    map.elements.Add(reflector);
                    break;
                case 5:
                    LoadingInputerObject inputerObject = new LoadingInputerObject(obj as InputerObject);
                    inputerObject.Init(obj.x, obj.y, obj.rotate, obj.objectType);
                    map.objects.Add(inputerObject);
                    map.elements.Add(inputerObject);
                    break;
            }
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

    public void LoadLevel(int targetStage, int targetLevel)
    {
        if (PuzzleIn.currentStage + 1 == 0 || PuzzleIn.currentLevel + 1 == 0)
        {
            Debug.LogWarning("Level " + (PuzzleIn.currentStage + 1) + "-" + (PuzzleIn.currentLevel + 1) + "cannot Found");
        }

        string loadPath = "Assets/Resources/Levels/Level " + (targetStage + 1) + "-" + (targetLevel + 1) + ".asset";
        levelData = (MapData)AssetDatabase.LoadAssetAtPath(loadPath, typeof(MapData));

        if (levelData == null)
        {
            Debug.LogWarning("Cannot Find Level's Data : " + loadPath);
            return;
        }
        ObjectLoading();
        //PuzzleIn.LoadComplete();
    }
    private void ObjectLoading()
    {
        for (int i = elementList.Count - 1; i >= 0; i--)
        {            
            Destroy(elementList[i].gameObject);
        }

        elementList.Clear();
        tileList.Clear();
        objectList.Clear();

        foreach (LoadingObject v in levelData.elements)
        {
            Object obj = ObjectCreate(v);
            obj.SaveObject();
        }

        GetMapMaximumSize();
        //TileArrayment();
        //ObjectArrayment();
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
        if (obj.objectType == 2)
            instance.GetComponent<Outputer>().lineColorTheme = obj.lineColorTheme;
        if (obj.objectType == 3)
            instance.GetComponent<Inputer>().lineColorTheme = obj.lineColorTheme;

        return instance.GetComponent<Object>();
    }

    /*private void TileArrayment()
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
    }*/

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

    public void ClearChecking()
    {
        if (inputerOperating == completeRequires)
        {
            Debug.Log("Claer");
            PuzzleIn.LevelClearEventGenerate();
        }
    }
}
