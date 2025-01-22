using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainTitleScreen;
    [SerializeField] private GameObject optionScreen;

    private void Awake()
    {
        mainTitleScreen.SetActive(true);
        optionScreen.SetActive(false);
    }

    private void mainTitle_OnOff(int active)
    {
        switch(active)
        {
            case 0:
                mainTitleScreen.SetActive(false);
                break;
            case 1:
                mainTitleScreen.SetActive(true);
                break;
        }
        
    }

    public void option_OnOff(int active)
    {
        switch (active)
        {
            case 0:
                mainTitle_OnOff(1);
                optionScreen.SetActive(false);
                break;
            case 1:
                mainTitle_OnOff(0);
                optionScreen.SetActive(true);
                break;
        }
    }
}
