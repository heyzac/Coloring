using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inputer : TileObject
{
    public int lineColorTheme;

    private void Start()
    {
        First();
        ValueInitialization();
        TileStatement();
        Statement();
    }

    private void ValueInitialization()
    {
        objectType = PuzzleIn.INPUTER_TYPE;
    }

    private void Statement()
    {
        Spritement();
    }

    private void Spritement()
    {
        GetComponent<Image>().sprite = puzzleIn.inputerOnLine[lineColorTheme];
    }

    public void Input()
    {
        Debug.Log("Inputer has been Operated");
    }
}
