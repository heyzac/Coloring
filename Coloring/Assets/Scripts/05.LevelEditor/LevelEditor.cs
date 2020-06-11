using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : MonoBehaviour
{
    public event EventHandler Load;

    public MapData levelData;

    public void MapSave()
    {
        Debug.Log("save!!");
    }

    public void MapLoad()
    {
        if (this.Load != null)
        {
            Load(this, EventArgs.Empty);
        }
    }
}
