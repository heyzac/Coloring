using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inputer : TileObject, IInputer
{
    public int lineColorTheme;

    public int SoundTheme { get => lineColorTheme; }

    private void Start()
    {
        First();
        ValueInitialization();
        CompleteRequirementAddition();
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

    public void CompleteRequirementAddition()
    {
        grid.completeRequires++;
    }

    public void Input()
    {
        grid.inputerOperating++;
        SoundAddition();
    }

    public void SoundAddition()
    {

    }
}
