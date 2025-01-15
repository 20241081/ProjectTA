using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Script : MonoBehaviour
{
    public static Vector3 position01 = new Vector3(-6, 4.5f, 20);
    public static Vector3 position02 = new Vector3(0, 4.5f, 20);
    public static Vector3 position03 = new Vector3(6, 4.5f, 20);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("½¯µå È¹µæ");
            Manager_GameScene.isActive_shield = true;
            this.gameObject.SetActive(false);
        }
    }
}
