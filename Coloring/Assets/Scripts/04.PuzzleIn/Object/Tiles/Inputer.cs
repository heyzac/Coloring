using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputer : TileObject
{
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
        colorTheme = PuzzleIn.nowTheme;
    }

    public void Input()
    {
        Debug.Log("Inputer has been Operated");
        //스프라이트 교체
        //방향 검사
        //색 검사
    }
}
