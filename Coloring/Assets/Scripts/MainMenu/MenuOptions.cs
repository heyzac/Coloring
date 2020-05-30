using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{

    [SerializeField]
    private MainMenu mainMenu;

    public void OnClickTextMenu(GameObject textObject)
    {
        int index = Array.IndexOf(MainMenu.menu, textObject);
        //Debug.Log(index);

        //textValue 값에 따라 씬전환
        switch (index)
        {
            case 0:
                //레벨 셀렉트
                SceneManager.LoadScene(index + 1);
                break;
            case 1:
                //에디터
                SceneManager.LoadScene(index + 1);
                break;
            case 2:
                //설정
                break;

            case 3:
                //종료
                Debug.Log("종료");
                Application.Quit();
                break;

            default:
                //오류
                Debug.Log("예상치 못한 오류");
                Application.Quit();
                break;
        }

        return;
    }
}
