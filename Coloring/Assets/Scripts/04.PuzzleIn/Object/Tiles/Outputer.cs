using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outputer : TileObject
{
    public int lineColorTheme;

    private Line line;
    private void Start()
    {
        Initialization();
        First();
        TileStatement();
        Spritement();
    }

    private void Initialization()
    {
        line = transform.GetChild(0).GetComponent<Line>();
        objectType = PuzzleIn.OUTPUTER_TYPE;
    }

    public void Output()
    {

    }

    private void Spritement()
    {
        GetComponent<Image>().sprite = puzzleIn.outputerOnLine[lineColorTheme];
    }
}
