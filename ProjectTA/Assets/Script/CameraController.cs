using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetX;       
    public float offsetY;   
    public float offsetZ;      

    public float cameraSpeed; 
    private Vector3 targetPos;

    // 참조 컴포넌트
    public GameObject Player;
    private PlayerController playerCtr;

    private void Awake()
    {
        offsetX = 0.0f;
        offsetY = 4f;
        offsetZ = -5.0f;

        Player = GameObject.FindWithTag("Player");
        playerCtr = Player.GetComponent<PlayerController>();

        cameraSpeed = playerCtr.moveSpeed;
    }

    // todo : 장애물 충돌 시 플레이어 이동 속도를 카메라 이동 속도에 대입

    private void Update()
    {
        switch (playerCtr.LINE)
        {
            case 0:
                offsetX = 1.0f;
                break;
            case 1:
                offsetX = 0f;
                break;
            case 2:
                offsetX = -1.0f;
                break;
        }

        targetPos = new Vector3(
            Player.transform.position.x + offsetX,
            Player.transform.position.y + offsetY,
            Player.transform.position.z + offsetZ
            );

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * cameraSpeed);
    }
}
