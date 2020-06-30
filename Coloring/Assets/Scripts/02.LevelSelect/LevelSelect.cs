using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //상수 선언
    public const int STAGE_MAX_COUNT = 3;

    //전역 변수 선언
    public static int presentStage = 0;
    public GameObject[] stages;

    //이미지 스프라이트
    public Sprite[] levelImageArr;
    public Sprite[] clearedLevelImageArr;
    public Sprite[] backgroundImageArr;
    public Sprite[] stageRightArrowImageArr;
    public Sprite[] stageLeftArrowImageArr;

    private void Awake()
    {
        Initialization();
    }

    private void Start()
    {
        StageActivation();
    }

    private void Initialization()
    {
        Array.Resize<GameObject>(ref stages, STAGE_MAX_COUNT);
        Array.Resize<Sprite>(ref levelImageArr, STAGE_MAX_COUNT);
        Array.Resize<Sprite>(ref clearedLevelImageArr, STAGE_MAX_COUNT);
        Array.Resize<Sprite>(ref backgroundImageArr, STAGE_MAX_COUNT);
        Array.Resize<Sprite>(ref stageRightArrowImageArr, STAGE_MAX_COUNT);
        Array.Resize<Sprite>(ref stageLeftArrowImageArr, STAGE_MAX_COUNT);
    }

    private void StageActivation()
    {
        for (int i = 0; i < STAGE_MAX_COUNT; i++)
        {
            if (i == presentStage)
                stages[i].SetActive(true);
            else
                stages[i].SetActive(false);
        }
    }

    public void StageChange()
    {
        switch (presentStage)
        {
            case 0:
                //스테이지 1로 넘어감

                break;
            case 1:
                //스테이지 2로 넘어감
                break;
            case 2:
                //스테이지 3로 넘어감
                break;
            case 3:
                //스테이지 4로 넘어감
                break;

            default:
                break;
        }

        for(int i = 0; i < STAGE_MAX_COUNT; i++)
        {
            if (presentStage == i)
                stages[i].SetActive(true);
            else
                stages[i].SetActive(false);
        }

        Debug.Log((presentStage + 1) + "번 스테이지로 이동");

        return;
    }
}
