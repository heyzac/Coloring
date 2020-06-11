using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : TileObject
{
    void Start()
    {
        First();
        Initialization();
        TileStatement();
    }

    private void Initialization()
    {
        objectType = 0;
    }
}
