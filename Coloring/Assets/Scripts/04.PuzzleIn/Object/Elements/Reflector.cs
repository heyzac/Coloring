using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : PuzzleObject
{
    void Start()
    {
        First();
        VariableInitialization();
        ObjectStatement();
    }

    private void VariableInitialization()
    {
        objectType = 4;
        spriteNormal = puzzleIn.reflectorNormal[PuzzleIn.nowTheme];
        spriteSelect = puzzleIn.reflectorSelect[PuzzleIn.nowTheme];
    }

    private void Reflect()
    {

    }
}
