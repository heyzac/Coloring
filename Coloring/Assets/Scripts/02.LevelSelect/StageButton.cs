using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    //변수 선언
    public LevelSelect levelSelect;

    public void OnClickStageButton(int arrowType)
    {
        StageSelect(arrowType);
        levelSelect.StageChange();
    }

    private void StageSelect(int arrowType)
    {
        LevelSelect.presentStage += arrowType;
    }

}
