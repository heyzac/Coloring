using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outputer : TileObject
{
    void Start()
    {
        First();
        Initialization();
        TileStatement();
    }

    private void Initialization()
    {
        objectType = 2;
    }

    private void Output()
    {

    }
}
