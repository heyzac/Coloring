using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputer : TileObject
{
    void Start()
    {
        First();
        Initialization();
        TileStatement();
    }

    private void Initialization()
    {
        objectType = 3;
    }

    private void Input()
    {

    }
}
