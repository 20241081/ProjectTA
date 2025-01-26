using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject titleUImanager;

    public static int currentScreen;

    public static bool titleScreen;
    public static bool optionScreen;

    private void Awake()
    {
        currentScreen = 1;

        titleScreen = true;
        optionScreen = false;
    }

    private void Update()
    {
        switch(currentScreen)
        {
            case 1:
                if (optionScreen)
                {
                    titleScreen = false;
                    optionScreen = true;
                    currentScreen = 2;

                    OpenOption();
                }
                break;
            case 2:
                if (titleScreen)
                {
                    titleScreen = true;
                    optionScreen = false;
                    currentScreen = 1;

                    closeOption();
                }
                break;
        }
    }

    public void OpenOption()
    {
        titleUImanager.GetComponent<TitleUIManager>().option_OnOff(1);
    }

    public void closeOption()
    {
        titleUImanager.GetComponent<TitleUIManager>().option_OnOff(0);
    }
}
