using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PuzzleObject : MonoBehaviour
{
    private float x;
    private float y;
    private float z;

    [SerializeField]
    public TileMapScript tileMapScript;

    public PuzzleObject()
    {

    }

    private void OnMouseDown()
    {
        x = tileMapScript.mouseX;
        y = tileMapScript.mouseY;
        transform.position = new UnityEngine.Vector2((x * (float)1.08), (y * (float)1.08));
        Debug.Log(x + "," + y + ", ");
    }

    private void OnMouseUp()
    {

    }

    private void OnMouseDrag()
    {
    }
}
