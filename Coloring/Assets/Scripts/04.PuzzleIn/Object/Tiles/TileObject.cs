using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TileObject : Object
{
    protected void TileStatement()
    {
        SaveTile();
    }

    private void SaveTile()
    {
        grid.tileList.Add(gameObject);
        grid.elementList.Add(gameObject);
    }
}
