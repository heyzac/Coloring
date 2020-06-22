using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : TileObject
{
    private void Start()
    {
        First();
        TileStatement();
        Initialization();
    }

    private void Initialization()
    {
        objectType = PuzzleIn.WALL_TYPE;
    }
}
