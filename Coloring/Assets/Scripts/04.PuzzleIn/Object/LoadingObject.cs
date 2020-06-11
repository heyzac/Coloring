using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LoadingObject
{
    public int x;
    public int y;
    public int rotate;
    public int objectType;

    public LoadingObject(int x, int y, int rotate, int objectType)
    {
        this.x = x;
        this.y = y;
        this.rotate = rotate;
        this.objectType = objectType;
    }
}
