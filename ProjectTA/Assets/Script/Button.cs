using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void gameExit()
    {
        Debug.Log("게임 종료");
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    public void ReStart()
    {
        Debug.Log("대기화면 보기");
        gameManager.renewFile = true;
    }
}
