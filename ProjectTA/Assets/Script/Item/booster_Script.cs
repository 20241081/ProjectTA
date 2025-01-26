using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booster_Script : MonoBehaviour
{
    public static Vector3 position01 = new Vector3(-6, 4.5f, 20);
    public static Vector3 position02 = new Vector3(0, 4.5f, 20);
    public static Vector3 position03 = new Vector3(6, 4.5f, 20);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("∫ŒΩ∫≈Õ »πµÊ");
            if (!Manager_GameScene.isActive_booster)
            {
                Manager_GameScene.trigger_booster = true;
                this.gameObject.SetActive(false);
            }
        }
    }
}
