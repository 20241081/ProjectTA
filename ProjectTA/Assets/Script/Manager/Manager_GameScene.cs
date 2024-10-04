using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_GameScene : MonoBehaviour
{
    public static int PlayerHP;
    public static float Score;
    public static bool Progressing; // 게임오버 여부
    float delta;
    float StandardTime; // 스코어 갱신 기준 시간

    public TMP_Text PlayerHPUI;
    public TMP_Text ScoreUI;
    private void Start()
    {
        Time.timeScale = 1f;
        Progressing = true;
        PlayerHP = 2;
        delta = 0f;
        StandardTime = 0.5f;
    }

    void Update()
    {
        PlayerHPUI.text = "HP : " + PlayerHP;
        ScoreUI.text = "Score : " + Score;

        if (!Progressing && Time.timeScale != 0f)   // 게임오버 시 실행
        {
            Time.timeScale = 0f;
            Debug.Log("게임오버");
        }

        if (PlayerHP == 0 && Progressing)  // 게임오버
        {
            Progressing = false;
        }

        delta += Time.deltaTime;    // 시간에 따른 스코어 증가
        if(delta >= StandardTime && Progressing)
        {
            Score += 1f;
            delta = 0f;
        }
    }
}
