using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void gameExit()
    {
        Debug.Log("���� ����");
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    public void ReStart()
    {
        Debug.Log("���ȭ�� ����");
        gameManager.renewFile = true;
    }
}
