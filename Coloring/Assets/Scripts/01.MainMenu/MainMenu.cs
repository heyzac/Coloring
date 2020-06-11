using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //상수 선언
    public const int MENU_MAX_COUNT = 4;

    //변수 선언
    public static int selectedMenu = 0;

    //오브젝트 선언 및 초기화
    public static GameObject[] menu;

    void Start()
    {
        Array.Resize<GameObject>(ref menu, MENU_MAX_COUNT);

        menu[0] = GameObject.Find("GameStart");
        menu[1] = GameObject.Find("LevelEditor");
        menu[2] = GameObject.Find("Setting");
        menu[3] = GameObject.Find("Exit");
        MenuController();
    }

    public void MenuController()
    {
        for (int i = 0; i < MENU_MAX_COUNT; i++)
        {
            if (selectedMenu == i)
            {
                menu[i].SetActive(true);
            }
            else {
                menu[i].SetActive(false);
            }
        }
    }
}
