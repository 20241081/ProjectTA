using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject Obstacle01;
    public GameObject Obstacle02;
    public GameObject Obstacle03;
    public GameObject Obstacle04;

    public GameObject Player;
    public Camera mainCamera;
    

    public float delta = 0;
    public float standardDist = 12.0f;  // ��ֹ� �������� �Ÿ�
    public float Distance = 0f;

    int Gen_Type = 0;   // ������ ��ֹ� ����
    int Gen_Rail = 0;   // ������ ��ֹ��� ������ ����

    void Update()
    {
        delta += Time.deltaTime;
        if (delta >= 0)
        {
            Distance += Time.deltaTime * Player.GetComponent<PlayerController>().moveSpeed;
            delta = 0;
        }
        if (Distance > standardDist)   // ��ֹ� ����
        {
            delta = 0;
            Distance = 0;
            Gen_Type = Random.Range(1, 5);    // 1~5
            switch (Gen_Type)
            {
                case 1:
                    Gen_Rail = Random.Range(-1, 1);   //-1~0
                    if (Gen_Rail == 0)
                    {
                        Gen_Rail = 1;
                    }
                    break;
                case 4:
                    Gen_Rail = 0;
                    break;
                default:
                    Gen_Rail = Random.Range(-1, 2);   //-1~0
                    break;
            }
            
            
            Create_Obstacle(Gen_Type, Gen_Rail);
            Debug.Log("��ֹ� ����");
        }

        DestroyOffScr();    // ȭ�� ���� �� ����
    }

    void Create_Obstacle(int Type, int Rail)
    {
        GameObject gameObstacle;

        float PositionZ = Player.transform.position.z + 15f;
        float PositionX = Rail * 6;
        switch (Type)
        {
            case 1:
                gameObstacle = Instantiate(Obstacle01);
                break;
            case 2:
                gameObstacle = Instantiate(Obstacle02);
                break;
            case 3:
                gameObstacle = Instantiate(Obstacle03);
                break;
            case 4:
                gameObstacle = Instantiate(Obstacle04);
                break;
            default:
                return;
        }

        if (gameObstacle != null)
        {
            var obstacleComponent = gameObstacle.GetComponent<Obstacle>();
            obstacleComponent.ObstacleType = Type;
            obstacleComponent.TargetRail = Rail + 1;
            gameObstacle.transform.position = new Vector3(PositionX, 4, PositionZ);

            //ȸ�� ����
            if (Rail == 1 && Type == 1)
            {
                gameObstacle.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }

    void DestroyOffScr()    // ������Ʈ ����
    {
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            if (IsOffscreen(obstacle))
            {
                Destroy(obstacle);
            }
        }
    }

    bool IsOffscreen(GameObject obstacle)   // ������ ���� ��ġ Ȯ��
    {
        Vector3 viewPortPos = mainCamera.WorldToViewportPoint(obstacle.transform.position);
        return viewPortPos.x < -1 || viewPortPos.x > 1 || viewPortPos.z < -600;
    }
}
