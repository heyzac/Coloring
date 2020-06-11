using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PuzzleIn : MonoBehaviour
{
    private const int MAX_THEME_COUNT = 3;

    public static int currentLevel = 0;
    public static int nowTheme = 0;

    public TileBase[] emptyTile;
    public TileBase[] wallTile;
    public Sprite[] reflectorNormal;
    public Sprite[] reflectorSelect;

    public GameObject[] gameElement;

    void Awake()
    {
        Array.Resize<TileBase>(ref emptyTile, MAX_THEME_COUNT);
        Array.Resize<TileBase>(ref wallTile, MAX_THEME_COUNT);
        Array.Resize<Sprite>(ref reflectorNormal, MAX_THEME_COUNT);
        Array.Resize<Sprite>(ref reflectorSelect, MAX_THEME_COUNT);
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
