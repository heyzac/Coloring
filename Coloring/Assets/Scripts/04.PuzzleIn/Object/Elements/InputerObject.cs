using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputerObject : PuzzleObject
{
    private void Start()
    {
        VariableInitialization();
    }

    private void VariableInitialization()
    {
        objectType = PuzzleIn.INPUTER_OBJECT_TYPE;
        spriteNormal = puzzleIn.inputerObject[PuzzleIn.nowTheme];
        spriteSelect = puzzleIn.inputerObjectSelect[PuzzleIn.nowTheme];
    }
}