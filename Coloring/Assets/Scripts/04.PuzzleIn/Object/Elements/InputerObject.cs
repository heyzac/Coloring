using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputerObject : PuzzleObject, IInputer
{
    public int lineColorTheme;
    public int SoundTheme { get => colorTheme; }


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
    public void CompleteRequirementAddition()
    {
        grid.completeRequires++;
    }

    public void Input()
    {
        CompleteRequirementAddition();
        SoundAddition();
    }

    public void SoundAddition()
    {
        throw new System.NotImplementedException();
    }
}