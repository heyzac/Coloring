using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClearPanel : MonoBehaviour
{
    private void Awake()
    {
        EventInitialization();
    }
    void Start()
    {
        Initialization();
    }

    private void EventInitialization()
    {
        PuzzleIn.EventLevelClear += LevelClear;
        PuzzleIn.EventObjectLoad += ActiveSetting;
    }

    private void Initialization()
    {

    }

    private void ActiveSetting()
    {
        gameObject.SetActive(false);
    }

    private void LevelClear()
    {
        gameObject.SetActive(true);
        if (PuzzleIn.currentLevel > PuzzleIn.stages[PuzzleIn.currentStage].lastClearedLevel)
        {
            PuzzleIn.stages[PuzzleIn.currentStage].lastClearedLevel++;
        }


    }

    public void OnAgainLevelButton()
    {

    }

    public void OnLevelSelectButton()
    {

    }

    public void OnNextLevelSelectButton()
    {
        PuzzleIn.currentLevel++;
        if (PuzzleIn.currentLevel == Stage.LEVEL_MAX_COUNT)
        {
            PuzzleIn.currentLevel = 0;
            LevelSelect.presentStage++;
            PuzzleIn.currentStage++;

            if (PuzzleIn.currentStage == LevelSelect.STAGE_MAX_COUNT)
            {
                PuzzleIn.currentStage--;
            }
        }

    }
}
