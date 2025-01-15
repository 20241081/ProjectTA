using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ÄÚÀÎ 1 È¹µæ");
            Manager_GameScene.coin_a += 1;
            DestroyCoin();
        }
    }

    private void DestroyCoin()
    {
        this.gameObject.SetActive(false);
    }
}
