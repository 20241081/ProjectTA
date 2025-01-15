using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Script : MonoBehaviour
{
    public static Vector3 position01 = new Vector3(-6, 4.5f, 20);
    public static Vector3 position02 = new Vector3(0, 4.5f, 20);
    public static Vector3 position03 = new Vector3(6, 4.5f, 20);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch(Manager_GameScene.PlayerHP)
            {
                case 1:
                    Debug.Log("체력 회복");
                    Manager_GameScene.PlayerHP += 1;
                    this.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("회복 무효");
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
