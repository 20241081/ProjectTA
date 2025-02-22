using Unity.VisualScripting;
using UnityEngine;

public class Obstacle_Stage02 : MonoBehaviour
{
    [SerializeField] private GameObject model;

    private bool interacted;

    private void Start()
    {
        interacted = false;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (!interacted)
        {
            Manager_GameScene.PlayerHP -= 1;
            interacted = true;
        }
    }
}