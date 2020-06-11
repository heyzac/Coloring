using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapScript : MonoBehaviour
{
    public Tilemap tilemap;

    GridLayout gridLayout;
    Vector3Int cellPosition;

    void Start()
    {
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition);
    }
}
