using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameOverPanel : MonoBehaviour
{
    public TMP_Text BestScoreText;
    int BestScore;
    void Start()
    {
        
    }

    void Update()
    {
        if (BestScore != gameManager.Instance.nowPlayer.BestScore)
        {
            BestScore = gameManager.Instance.nowPlayer.BestScore;
            BestScoreText.text = "Best Score : " + BestScore;
        }
    }
}
