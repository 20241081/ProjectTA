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
    public static bool isActive_shield; // ���� Ȱ��ȭ ����
    public static bool isActive_booster;    // �ν��� Ȱ��ȭ ����
    public static bool trigger_booster; // �ν��� ȹ�� ����
    public static int coin_a;   // �÷��� �� ȹ���� ����
    public static bool Progressing; // ���ӿ��� ����
    float delta;    // �ʰ��ð� ���

    private int ScorePerDistance;   // �Ÿ��� ���� ���ھ�

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
        isActive_shield = false;
        trigger_booster = false;
        coin_a = 0;
        player = GameObject.Find("Player");
        GameOverPanel = GameObject.Find("GameOverPanel");
        GameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!Progressing && Time.timeScale != 0f)   // ���ӿ��� ��
        {
            Time.timeScale = 0f;
            Debug.Log("���ӿ���");
            GameOverPanel.SetActive(true); // ���ӿ��� UI ǥ��
            if (gameManager.Instance.nowPlayer.BestScore < Score)   // �ְ� ���ھ� �˻�
            {
                gameManager.Instance.nowPlayer.BestScore = Score;
            }
            gameManager.Instance.nowPlayer.Coin += coin_a;
            coin_a = 0;
            return;
        }

        PlayerHPUI.text = "HP : " + PlayerHP;   // HP UI������Ʈ

        CoinUI.text = "coin : " + coin_a;  // coin UI ������Ʈ

        if (PlayerHP <= 0 && Progressing)  // ���ӿ���
        {
            Progressing = false;
        }

        delta += Time.deltaTime;    // �Ÿ��� ���� ���ھ� ������Ʈ
        if (delta >= 1)
        {
            ScorePerDistance += (int)delta * (int)player.transform.GetComponent<PlayerController>().moveSpeed;
            delta = 0;
        }
        Score = ScorePerDistance;

        ScoreUI.text = "Score : " + Score;  // Score UI ������Ʈ

        if (!isActive_booster && trigger_booster)
        {
            //isActive_booster = true;
            //Invoke("Speedreset", 5.0f);
        }
    }

    void Speedreset()
    {
        isActive_booster = false;
    }
}
