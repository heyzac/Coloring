using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Object : MonoBehaviour
{
    //상태 값
    public int x;
    public int y;
    public int z;
    public int rotate; //0 ~ 3
    public int objectType;
    public Vector3Int pos;
    protected float xReal;
    protected float yReal;

    //스프라이트 선언
    protected Sprite spriteNormal;
    protected Sprite spriteSelect;

    //게임 오브젝트 선언

    //컴포넌트 선언
    protected Tilemap tilemap;
    protected GridScript grid;
    protected PuzzleIn puzzleIn;
    protected MapData mapData;

    public void First()
    {
        Initialization();
        PositionInit();
        RotatementInit();
    }

    private void Initialization()
    {
        //컴포넌트 초기화
        GameObject gridObject = GameObject.Find("Grid");
        puzzleIn = GameObject.Find("ScriptObject").GetComponent<PuzzleIn>();
        tilemap = gridObject.transform.GetChild(0).GetComponent<Tilemap>();
        grid = gridObject.GetComponent<GridScript>();
        mapData = grid.levelData;

        //변수 초기화
        z = 0;
    }

    private void PositionInit()
    {
        pos = GetPosition();
        transform.position = GetRealPosition(pos.x, pos.y);
    }
    private void RotatementInit()
    {
        Quaternion quart;
        quart = Quaternion.Euler(0, 0, 90 * rotate);
        GetComponent<Transform>().rotation = quart;
    }

    public Vector3Int GetPosition()
    {
        return new Vector3Int(x, y, z);
    }

    public Vector3 GetRealPosition(int xx, int yy)
    {
        xReal = (xx * 1.08f) + 0.5f;
        yReal = (yy * 1.08f) + 0.5f;
        return new Vector3(xReal, yReal, 0);
    }

    public Quaternion GetRotation()
    {
        Quaternion quart;
        quart = Quaternion.Euler(0, 0, 90 * rotate);        
        return quart;
    }
}
