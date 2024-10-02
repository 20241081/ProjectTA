using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_GameScene : MonoBehaviour
{
    public static int PlayerHP;

    public TMP_Text PlayerHPUI;
    private void Start()
    {
        PlayerHP = 2;
    }

    void Update()
    {
        PlayerHPUI.text = "HP : " + PlayerHP;
        if (PlayerHP == 0 && Time.timeScale != 0f)
        {
            Debug.Log("게임오버");
            Time.timeScale = 0f;
        }
    }
}
