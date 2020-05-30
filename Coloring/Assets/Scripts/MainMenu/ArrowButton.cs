using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowButton : MonoBehaviour
{
    //스크립트 선언
    [SerializeField]
    private MainMenu mainMenu;

    //변수 선언
    private int menuValue;

    public void OnClickArrowButton(int buttonValue)
    {
        menuValue = MainMenu.selectedMenu;

        switch (buttonValue)
        {
            case 0:
                menuValue -= 1;

                if (menuValue < 0)
                    menuValue = (MainMenu.MENU_MAX_COUNT - 1);
                break;

            case 1:
                menuValue += 1;

                if (menuValue == MainMenu.MENU_MAX_COUNT)
                    menuValue = 0;
                break;

            default:
                break;
        }

        MainMenu.selectedMenu = menuValue;
        mainMenu.MenuController();
        return;
    }

}
