using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : TileObject
{
    private void Start()
    {
        First();
        Initialization();
        TileStatement();
    }

    private void Initialization()
    {
        objectType = PuzzleIn.EMPTY_TYPE;
    }
}
