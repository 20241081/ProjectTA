using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_GameScene : MonoBehaviour
{
    GameObject player;
    GameObject GameOverPanel;

    public static int PlayerHP;
    public static int Score;
    public static bool Progressing; // 게임오버 여부
    float delta;    // 초과시간 기록

    private int ScorePerDistance;   // 거리에 따른 스코어

    public TMP_Text PlayerHPUI;
    public TMP_Text ScoreUI;
    public TMP_Text CoinUI;
    private void Start()
    {
        Time.timeScale = 1f;
        Progressing = true;
        PlayerHP = 2;
        delta = 0f;
        ScorePerDistance = 0;
        player = GameObject.Find("Player");
        GameOverPanel = GameObject.Find("GameOverPanel");
        GameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!Progressing && Time.timeScale != 0f)   // 게임오버 시
        {
            Time.timeScale = 0f;
            Debug.Log("게임오버");
            GameOverPanel.SetActive(true); // 게임오버 UI 표시
            if (gameManager.Instance.nowPlayer.BestScore < Score)   // 최고 스코어 검사
            {
                gameManager.Instance.nowPlayer.BestScore = Score;
            }
            return;
        }

        PlayerHPUI.text = "HP : " + PlayerHP;   // HP UI업데이트

        CoinUI.text = "coin : " + gameManager.Instance.nowPlayer.Coin;  // coin UI 업데이트

        if (PlayerHP <= 0 && Progressing)  // 게임오버
        {
            Progressing = false;
        }

        delta += Time.deltaTime;    // 거리에 따른 스코어 업데이트
        if (delta >= 1)
        {
            ScorePerDistance += (int)delta * (int)player.transform.GetComponent<PlayerController>().moveSpeed;
            delta = 0;
        }
        Score = ScorePerDistance;

        ScoreUI.text = "Score : " + Score;  // Score UI 업데이트
    }
}
