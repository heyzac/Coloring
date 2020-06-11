using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : TileObject
{
    void Start()
    {
        First();
        TileStatement();
        Initialization();
    }

    private void Initialization()
    {
        objectType = 1;
    }
}
