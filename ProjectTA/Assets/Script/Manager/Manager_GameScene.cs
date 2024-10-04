using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_GameScene : MonoBehaviour
{
    public static int PlayerHP;
    public static float Score;
    public static bool Progressing; // ���ӿ��� ����
    float delta;
    float StandardTime; // ���ھ� ���� ���� �ð�

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

        if (!Progressing && Time.timeScale != 0f)   // ���ӿ��� �� ����
        {
            Time.timeScale = 0f;
            Debug.Log("���ӿ���");
        }

        if (PlayerHP == 0 && Progressing)  // ���ӿ���
        {
            Progressing = false;
        }

        delta += Time.deltaTime;    // �ð��� ���� ���ھ� ����
        if(delta >= StandardTime && Progressing)
        {
            Score += 1f;
            delta = 0f;
        }
    }
}
