using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    //클래스 선언
    public Stage stage = new Stage();

    //변수 선언
    private bool isCleared;
    public int levelNumber;

    //게임오브젝트 선언
    public GameObject thisLevelObject;
    public GameObject levelTextObject;

    void Awake()
    {
        
    }

    void Start()
    {
        TextSetting();
    }

    private void TextSetting()
    {
        //levelTextObject = transform.Find("Text").gameObject;
        levelTextObject.GetComponent<Text>().text = "" + (levelNumber + 1);

    }

    //클리어 여부에 따라 행동을 결정
    private void clearCheck()
    {
        //이 스테이지가 클리어 된 것인지 확인
        if (stage.lastClearedLevel <= levelNumber)
            isCleared = true;
        else
            isCleared = false;

        if (isCleared == true)
        {
            //추가 행동
        }
    }

    public void OnLevelButtonClick()
    {
        //사전 작업

        //로딩씬 로드
        SceneManager.LoadScene("SceneLoading");

        return;
    }
}
