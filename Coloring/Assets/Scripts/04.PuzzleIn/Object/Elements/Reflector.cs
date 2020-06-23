using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reflector : PuzzleObject
{
    private void Start()
    {
        EventInitialization();

        First();
        VariableInitialization();
        ObjectStatement();
    }

    private void EventInitialization()
    {

    }

    private void VariableInitialization()
    {
        objectType = PuzzleIn.REFLECTOR_TYPE;
        spriteNormal = puzzleIn.reflector[PuzzleIn.nowTheme];
        spriteSelect = puzzleIn.reflectorSelect[PuzzleIn.nowTheme];
    }

    public int Reflect(int lineDirect)
    {
        int result = -1;

        if (rotate == lineDirect)
            result = grid.RotateOverflow(lineDirect + 1);
        else if (rotate == grid.RotateOverflow(lineDirect + 1))
            result = grid.RotateOverflow(lineDirect - 1);

        return result;
    }

    public void SpriteChangeFromLine(int lineColorTheme)
    {
        Debug.Log("hey");
        spriteOnLine = puzzleIn.reflectorOnLine[lineColorTheme];
        GetComponent<Image>().sprite = spriteOnLine;
    }
}
