using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    //상수 선언
    public const int LEVEL_MAX_COUNT = 12;

    //전역 변수 선언
    public int stageNumber;

    //변수 선언
    public int lastClearedLevel;

    //클래스 선언
    public LevelSelect levelSelect;

    //스프라이트 선언
    public Sprite levelImage;
    public Sprite clearedLevelImage;
    public Sprite backgroundImage;
    public Sprite stageRightArrowImage;
    private Sprite stageLeftArrowImage;

    //게임오브젝트 선언
    public GameObject[] levels;

    public GameObject background;
    public GameObject stageText;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject returnText;

    void Awake()
    {
        Initialization();
        
    }

    void Start()
    {
        StageSettlement();
        LevelActivation();
    }

    private void Initialization()
    {
        //이미지 스프라이트 소스 초기화
        levelImage = levelSelect.levelImageArr[stageNumber];
        clearedLevelImage = levelSelect.clearedLevelImageArr[stageNumber];
        backgroundImage = levelSelect.backgroundImageArr[stageNumber];
        stageRightArrowImage = levelSelect.stageRightArrowImageArr[stageNumber];
        stageLeftArrowImage = levelSelect.stageLeftArrowImageArr[stageNumber];

        //게임 오브젝트 자동으로 찾기
        for (int i = 0; i < LEVEL_MAX_COUNT; i++)
        {
            string str = "Level (" + (i + 1) + ")";
            levels[i] = transform.Find(str).gameObject;
            levels[i].GetComponent<LevelButton>().levelNumber = i;
        }

        background = transform.Find("Background").gameObject;
        stageText = transform.Find("StageText").gameObject;
        leftArrow = transform.Find("LeftArrow").gameObject;
        rightArrow = transform.Find("RightArrow").gameObject;
        returnText = transform.Find("Return").gameObject;

        return;
    }

    private void StageSettlement()
    {
        //이미지 스프라이트 세팅
        background.GetComponent<Image>().sprite = backgroundImage;
        leftArrow.GetComponent<Image>().sprite = stageLeftArrowImage;
        rightArrow.GetComponent<Image>().sprite = stageRightArrowImage;

        //처음 또는 마지막 스테이지라면 스테이지 이동 버튼 하나 삭제
        if (stageNumber == 0)
            leftArrow.SetActive(false);
        else if (stageNumber == (LevelSelect.STAGE_MAX_COUNT - 1))
            rightArrow.SetActive(false);

        stageText.GetComponent<Text>().text = ("Stage " + (stageNumber + 1));

        return;
    }

    //재점검 및 수정 요망
    private void LevelActivation()
    {
        for (int i = 0; i < lastClearedLevel; i++)
        {
            levels[i].GetComponent<Image>().sprite = clearedLevelImage;
        }

        for (int i = lastClearedLevel; i < LEVEL_MAX_COUNT; i++)
        {
            levels[i].GetComponent<Image>().sprite = levelImage;
            
            if (i == lastClearedLevel)
                levels[i].SetActive(true);
            else
                levels[i].SetActive(false);
        }

        return;
    }
}
