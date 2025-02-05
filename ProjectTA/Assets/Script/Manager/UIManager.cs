using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject progressingUI;
    [SerializeField] private GameObject shopScreen;

    [SerializeField] private GameObject menuButtons;

    [SerializeField] private TMP_Text playerHP_Text;
    [SerializeField] private TMP_Text Score_Text;
    [SerializeField] private TMP_Text Coin_Text;

    [SerializeField] private GameObject menuBackGround;

    [SerializeField] private GameObject menuScreen;

    [SerializeField] private TMP_Text Menu_BestScore_Text;
    [SerializeField] private TMP_Text Menu_Coin_Text;

    private void Awake()
    {
        GameOverPanel.SetActive(false);
        shopScreen.SetActive(false);
    }

    private void Update()
    {
        if (Manager_GameScene.Progressing)
        {
            Score_Text.text = "Score : " + Manager_GameScene.Score;  // Score UI ������Ʈ
            playerHP_Text.text = "HP : " + Manager_GameScene.PlayerHP;   // HP UI������Ʈ
            Coin_Text.text = "coin : " + (Manager_GameScene.coin_a + gameManager.Instance.nowPlayer.Coin);  // coin UI ������Ʈ
        }
    }

    public void gameoverPanel_OnOff(int active)
    {
        switch (active)
        {
            case 0:
                GameOverPanel.SetActive(false);
                break;
            case 1:
                GameOverPanel.SetActive(true);
                break;
        }
    }

    public void menuScreen_OnOff(int active)
    {
        switch (active)
        {
            case 0:
                menuScreen.SetActive(false);
                backGround_OnOff(0);
                break;
            case 1:
                menuScreen.SetActive(true);
                Menu_BestScore_Text.text = "Best Score : " + gameManager.Instance.nowPlayer.BestScore;
                Menu_Coin_Text.text = "Coin : " + gameManager.Instance.nowPlayer.Coin.ToString();
                backGround_OnOff(1);
                break;
        }
    }

    private void backGround_OnOff(int active)
    {
        switch (active)
        {
            case 0:
                menuBackGround.SetActive(false);
                break;
            case 1:
                menuBackGround.SetActive(true);
                break;
        }
    }

    public void ProgressingUI_OnOff(int active)
    {
        switch (active)
        {
            case 0:
                progressingUI.SetActive(false);
                break;
            case 1:
                progressingUI.SetActive(true);
                break;
        }
    }

    public void menuAndShopOnOff(int active)
    {
        switch (active)
        {
            case 0:
                menuButtons.SetActive(false);
                shopScreen.SetActive(true);
                break;
            case 1:
                menuButtons.SetActive(true);
                shopScreen.SetActive(false);
                break;
        }
    }
}
