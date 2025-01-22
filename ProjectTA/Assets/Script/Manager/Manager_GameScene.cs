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
    public static bool isActive_shield; // 쉴드 활성화 여부
    public static bool isActive_booster;    // 부스터 활성화 여부
    public static bool trigger_booster; // 부스터 획득 여부
    public static int coin_a;   // 플레이 중 획득한 코인

    public static bool Progressing; // 게임 진행 여부
    public static bool isGameOver;  // 게임오버 여부
    private bool gameoverScreenOpened;
    public static bool isScreenMenu;    // 메뉴 화면 여부
    private bool menuScreenOpened;

    float delta;    // 초과시간 기록
    public static int activedObstacleRail; // 현재 장애물 있는 레일;

    private int ScorePerDistance;   // 거리에 따른 스코어

    public TMP_Text PlayerHPUI;
    public TMP_Text ScoreUI;
    public TMP_Text CoinUI;
    private void Start()
    {
        statusReset();
    }

    void Update()
    {
        if (!Progressing)
        {
            // 게임오버 시
            if (isGameOver && !gameoverScreenOpened)
            {
                Debug.Log("게임오버");
                openGameOver();
                UIManager.GetComponent<UIManager>().gameoverPanel_OnOff(1);
                if (gameManager.Instance.nowPlayer.BestScore < Score)   // 최고 스코어 검사
                {
                    gameManager.Instance.nowPlayer.BestScore = Score;
                }
                gameManager.Instance.nowPlayer.Coin += coin_a;
                coin_a = 0;
                gameoverScreenOpened = true;
                return;
            }
            // 메뉴 화면
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
                Debug.Log("게임 시작");
                gameStart();
                UIManager.GetComponent<UIManager>().ProgressingUI_OnOff(1);
                UIManager.GetComponent<UIManager>().menuScreen_OnOff(0);
            }
            else
            {
                if (PlayerHP <= 0 && Progressing)  // 게임오버
                {
                    Progressing = false;
                    isGameOver = true;
                }

                delta += Time.deltaTime;    // 거리에 따른 스코어 업데이트
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
