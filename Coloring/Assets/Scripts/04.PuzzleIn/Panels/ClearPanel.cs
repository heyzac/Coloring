using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClearPanel : MonoBehaviour
{
    public GridScript grid;
    private void Awake()
    {
        EventInitialization();
        Initialization();
    }
    void Start()
    {

    }

    private void EventInitialization()
    {
        PuzzleIn.EventLevelClear += LevelClear;
        PuzzleIn.EventObjectLoad += ActiveSetting;
    }

    private void Initialization()
    {
        //grid = transform.parent.Find("Grid").GetComponent<GridScript>();
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
        gameObject.SetActive(false);
        grid.LoadLevel(PuzzleIn.currentStage, PuzzleIn.currentLevel);
    }

    public void OnLevelSelectButton()
    {

    }

    public void OnNextLevelSelectButton()
    {
        gameObject.SetActive(false);
        if (++PuzzleIn.currentLevel == Stage.LEVEL_MAX_COUNT)
        {
            PuzzleIn.currentLevel = 0;
            LevelSelect.presentStage++;
            PuzzleIn.currentStage++;

            if (PuzzleIn.currentStage == LevelSelect.STAGE_MAX_COUNT)
            {
                PuzzleIn.currentStage--;
            }
        }

        grid.LoadLevel(PuzzleIn.currentStage, PuzzleIn.currentLevel);
    }
}
