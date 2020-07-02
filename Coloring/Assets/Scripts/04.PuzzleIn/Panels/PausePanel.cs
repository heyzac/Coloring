using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnResumeTextButton()
    {
        Debug.Log("Game has been Resumed");
    }

    public void OnResolveLevelTextButton()
    {
        Debug.Log("Level has been ReLoaded");
    }

    public void OnOptionTextButton()
    {
        Debug.Log("Pop up the Option Panel");
    }

    public void OnLevelSelectTextButton()
    {
        Debug.Log("Move to LevelSelect");
    }
}
