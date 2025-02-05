using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject scenemanager;
    [SerializeField] private GameObject UImanager;

    public void TitleToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOption()
    {
        TitleManager.titleScreen = false;
        TitleManager.optionScreen = true;
    }

    public void CloseOption()
    {
        TitleManager.titleScreen = true;
        TitleManager.optionScreen = false;
    }

    public void gameExit()
    {
        Debug.Log("���� ����");
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    public void ReStart()
    {
        Debug.Log("���ȭ�� ����");
        gameManager.renewFile = true;
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void exitMenu()
    {
        Manager_GameScene.Progressing = true;
    }

    public void menuToShop()
    {
        UImanager.GetComponent<UIManager>().menuAndShopOnOff(0);
    }

    public void ShopToMenu()
    {
        UImanager.GetComponent<UIManager>().menuAndShopOnOff(1);
    }
}
