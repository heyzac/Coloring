using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuzzleIn : MonoBehaviour
{
    //이벤트 핸들러
    public delegate void GameStatement();
    public static event GameStatement LoadComplete;

    //수 많은 상수
    private const int MAX_THEME_COUNT = 3;
    private const int MAX_LINE_COLOR_COUNT = 3;

    public const int OBJECT_AND_TILE_BOUNDARY = 3;

    public const int EMPTY_TYPE = 0;
    public const int WALL_TYPE = 1;
    public const int OUTPUTER_TYPE = 2;
    public const int INPUTER_TYPE = 3;
    public const int REFLECTOR_TYPE = 4;
    public const int INPUTER_OBJECT_TYPE = 5;

    //수 많은(예정) 전역 변수
    public static float gridAdjusment = 0.5f;

    //상태 전역 변수
    public static bool isMouseDragging = false;
    public static bool isMousePressing = false;

    public static int currentLevel = 0;
    public static int nowTheme = 0;

    //스프라이트 저장 배열
    //default
    public Sprite[] wallTile;
    public Sprite[] emptyTile;
        //public Sprite[] outputer;
        //public Sprite[] inputer;
    public Sprite[] reflector;
    public Sprite[] inputerObject;

    //on line
        //public Sprite[] outputerOnLine;
        //public Sprite[] inputerOnLine;
    public Sprite[] reflectorOnLine;
    public Sprite[] inputerObjectOnLine;

    //select
    public Sprite[] reflectorSelect;
    public Sprite[] inputerObjectSelect;

    //프리팹 저장
    public GameObject[] gameElement;

    void Awake()
    {
        Array.Resize<Sprite>(ref wallTile, MAX_THEME_COUNT);
        Array.Resize<Sprite>(ref emptyTile, MAX_THEME_COUNT);
        //Array.Resize<Sprite>(ref outputer, MAX_THEME_COUNT);
        //Array.Resize<Sprite>(ref inputer, MAX_THEME_COUNT);
        Array.Resize<Sprite>(ref reflector, MAX_THEME_COUNT);
        Array.Resize<Sprite>(ref inputerObject, MAX_THEME_COUNT);

        //Array.Resize<Sprite>(ref outputerOnLine, MAX_LINE_COLOR_COUNT);
        //Array.Resize<Sprite>(ref inputerOnLine, MAX_LINE_COLOR_COUNT);
        Array.Resize<Sprite>(ref reflectorOnLine, MAX_LINE_COLOR_COUNT);
        Array.Resize<Sprite>(ref inputerObjectOnLine, MAX_LINE_COLOR_COUNT);

        Array.Resize<Sprite>(ref reflectorSelect, MAX_THEME_COUNT);
        Array.Resize<Sprite>(ref inputerObjectSelect, MAX_THEME_COUNT);
    }

    void Start()
    {
        LoadComplete();
    }

}
