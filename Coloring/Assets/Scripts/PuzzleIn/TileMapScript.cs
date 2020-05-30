using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapScript : MonoBehaviour
{
    private Matrix4x4 matrix0 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
    private Matrix4x4 matrix90 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
    private Matrix4x4 matrix180 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 180f), Vector3.one);
    private Matrix4x4 matrix270 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 270f), Vector3.one);

    public float mouseX;
    public float mouseY;
    public float mouseZ;
    public Tilemap tilemap;

    public TileBase wallTile;
    public TileBase selectNormalTile;
    public TileBase deselectNormalTile;
    public TileBase selectReflectTile;
    public TileBase deselectReflectTile;

    public Vector3Int v3Int;
    public Vector3Int tileV3Int;
    public Vector3Int objectV3Int;

    private TileBase saveTile;
    private TileBase saveObject;
    private TileBase thisTile;

    public Ray ray;
    GridLayout gridLayout;
    Vector3Int cellPosition;

    // Start is called before the first frame update
    void Start()
    {
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition);

        TileVectorInit();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mouseX = tilemap.WorldToCell(ray.origin).x;
        mouseY = tilemap.WorldToCell(ray.origin).y;
    }

    //추후 대규모 수정 필요
    private void OnMouseDown()
    {
        v3Int = new Vector3Int((int)mouseX, (int)mouseY, 0);

        thisTile = tilemap.GetTile(v3Int);
        saveTile = tilemap.GetTile(tileV3Int);
        saveObject = tilemap.GetTile(objectV3Int);

        NormalTileChecking(deselectNormalTile, selectNormalTile);
        ObjectTileChecking(deselectReflectTile, selectReflectTile);
    }

    private void NormalTileChecking(TileBase tileA, TileBase tileB)
    {
        if (thisTile == tileA)
        {
            if (saveTile == tileB)
            {
                tilemap.SetTile(tileV3Int, tileA);
            }

            tilemap.SetTile(v3Int, tileB);
            tileV3Int = v3Int;

            if (saveObject == selectReflectTile)
            {
                TileChanging();
            }
        }

        else if (thisTile == tileB)
        {
            tilemap.SetTile(v3Int, tileA);
            tileV3Int = new Vector3Int();
        }
    }

    private void ObjectTileChecking(TileBase tileA, TileBase tileB)
    {
        if (thisTile == tileA)
        {
            if (saveTile == tileB)
            {
                tilemap.SetTile(tileV3Int, tileA);
            }

            tilemap.SetTile(v3Int, tileB);
            objectV3Int = v3Int;

            if (saveTile == selectNormalTile)
            {
                TileChanging();
            }

        }
        else if (thisTile == tileB)
        {
            tilemap.SetTile(v3Int, tileA);
            rotateTile();
            objectV3Int = new Vector3Int();
        }
    }

    private void TileChanging()
    {
        Matrix4x4 mat = tilemap.GetTransformMatrix(objectV3Int);

        tilemap.SetTile(tileV3Int, deselectNormalTile);
        tilemap.SetTile(objectV3Int, deselectReflectTile);

        saveTile = tilemap.GetTile(tileV3Int);
        saveObject = tilemap.GetTile(objectV3Int);

        tilemap.SetTile(tileV3Int, saveObject);
        tilemap.SetTile(objectV3Int, saveTile);

        tilemap.SetTransformMatrix(tileV3Int, mat);

        TileVectorInit();
    }

    private void TileVectorInit()
    {
        saveTile = null;
        saveObject = null;
        tileV3Int = new Vector3Int();
        objectV3Int = new Vector3Int();
    }

    private void rotateTile()
    {
        Matrix4x4 thisMatrix = tilemap.GetTransformMatrix(objectV3Int);

        if (thisMatrix == matrix0)
            thisMatrix = matrix90;
        else if (thisMatrix == matrix90)
            thisMatrix = matrix180;
        else if (thisMatrix == matrix180)
            thisMatrix = matrix270;
        else if (thisMatrix == matrix270)
            thisMatrix = matrix0;

        tilemap.SetTransformMatrix(objectV3Int, thisMatrix);
    }
}
