using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

public class Manager_GameScene : MonoBehaviour
{
    GameObject player;
    [SerializeField] private GameObject UIManager;
    GameObject GameOverPanel;

    public static int PlayerHP;
    public static int Score;
    public static int currentStage;
    public static bool isActive_shield; // ���� Ȱ��ȭ ����
    public static bool isActive_booster;    // �ν��� Ȱ��ȭ ����
    public static bool trigger_booster; // �ν��� ȹ�� ����
    public static int coin_a;   // �÷��� �� ȹ���� ����

    public static bool Progressing; // ���� ���� ����
    public static bool isGameOver;  // ���ӿ��� ����
    public static bool isShopOpened;
    private bool gameoverScreenOpened;
    public static bool isScreenMenu;    // �޴� ȭ�� ����
    private bool menuScreenOpened;

    float delta;    // �ʰ��ð� ���
    public static int activedObstacleRail; // ���� ��ֹ� �ִ� ����;

    private int ScorePerDistance;   // �Ÿ��� ���� ���ھ�
    private void Start()
    {
        statusReset();
    }

    void Update()
    {
        if (!Progressing)
        {
            // ���ӿ��� ��
            if (isGameOver && !gameoverScreenOpened)
            {
                Debug.Log("���ӿ���");
                openGameOver();
                UIManager.GetComponent<UIManager>().gameoverPanel_OnOff(1);
                if (gameManager.Instance.nowPlayer.BestScore < Score)   // �ְ� ���ھ� �˻�
                {
                    gameManager.Instance.nowPlayer.BestScore = Score;
                }
                gameManager.Instance.nowPlayer.Coin += coin_a;
                coin_a = 0;
                gameoverScreenOpened = true;
                return;
            }
            // �޴� ȭ��
            else if (isScreenMenu)
            {
                if (!menuScreenOpened)
                {
                    openMenu();
                }
            }
        }
        else
        {
            if (gameoverScreenOpened || menuScreenOpened)
            {
                Debug.Log("���� ����");
                gameStart();
                UIManager.GetComponent<UIManager>().ProgressingUI_OnOff(1);
                UIManager.GetComponent<UIManager>().menuScreen_OnOff(0);
            }
            else
            {
                if (PlayerHP <= 0 && Progressing)  // ���ӿ���
                {
                    Progressing = false;
                    isGameOver = true;
                }

                delta += Time.deltaTime;    // �Ÿ��� ���� ���ھ� ������Ʈ
                if (delta >= 1)
                {
                    ScorePerDistance += (int)delta * (int)player.transform.GetComponent<PlayerController>().moveSpeed;
                    delta = 0;
                }
                Score = ScorePerDistance;
            }
        }
    }

    public void openMenu()
    {
        UIManager.GetComponent<UIManager>().menuScreen_OnOff(1);
        UIManager.GetComponent<UIManager>().ProgressingUI_OnOff(0);
        player.SetActive(false);
        GetComponent<Pool_Item>().enabled = false;
        GetComponent<obstaclePooler>().enabled = false;
        menuScreenOpened = true;
    }

    public void openGameOver()
    {
        GetComponent<Pool_Item>().enabled = false;
        GetComponent<obstaclePooler>().enabled = false;
    }    

    public void gameStart()
    {
        Time.timeScale = 1f;

        Progressing = true;
        isGameOver = false;
        isScreenMenu = false;

        gameoverScreenOpened = false;
        menuScreenOpened = false;

        PlayerHP = 2;
        delta = 0f;
        ScorePerDistance = 0;
        isActive_shield = false;
        trigger_booster = false;
        coin_a = 0;
        player.SetActive(true);
        GetComponent<Pool_Item>().enabled = true;
        GetComponent<obstaclePooler>().enabled = true;
    }

    private void statusReset()
    {
        Time.timeScale = 1f;

        Progressing = false;
        isGameOver = false;
        isScreenMenu = true;
        isShopOpened = false;

        currentStage = 0;

        gameoverScreenOpened = false;
        menuScreenOpened = false;

        PlayerHP = 2;
        delta = 0f;
        ScorePerDistance = 0;
        isActive_shield = false;
        trigger_booster = false;
        coin_a = 0;
        player = GameObject.Find("Player");
    }
}
