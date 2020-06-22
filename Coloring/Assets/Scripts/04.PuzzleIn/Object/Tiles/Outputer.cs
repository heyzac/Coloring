using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outputer : TileObject
{
    public int lineColorTheme = 1;

    private Line line;

    private void Start()
    {
        First();
        Initialization();
        TileStatement();
    }

    private void Initialization()
    {
        line = transform.GetChild(0).GetComponent<Line>();
        line.grid = this.grid;
        objectType = PuzzleIn.OUTPUTER_TYPE;
    }

    public void Output()
    {

    }
}
