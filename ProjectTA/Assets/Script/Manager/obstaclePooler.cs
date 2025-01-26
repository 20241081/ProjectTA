using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclePooler : MonoBehaviour
{
    [Header("장애물 생성 쿨타임")]
    [SerializeField] private float coolTime_Obstacle;
    [SerializeField] private GameObject player;

    private float deltaTIme_obstacle;
    private float distance;
    private float playerSpeed;

    public ObstaclePool obsPool;
    public GameObject obs1;
    public GameObject obs2;
    public GameObject obs3;
    public GameObject obs4;

    private Vector3 pos1 = new Vector3(-6, 4.5f, 20);
    private Vector3 pos2 = new Vector3(0, 4.5f, 20);
    private Vector3 pos3 = new Vector3(6, 4.5f, 20);

    void Update()
    {
        playerSpeed = player.GetComponent<PlayerController>().moveSpeed;
        gen();
    }

    private void gen()
    {
        if (distance > coolTime_Obstacle)
        {
            deltaTIme_obstacle = 0;
            distance = 0;
            int Pattern = Random.Range(0, 4);
            int pos = 0;
            switch (Pattern)
            {
                case 0:
                    pos = Random.Range(0, 2);
                    GameObject obj1 = obsPool.GetObject(obs1);
                    switch(pos)
                    {
                        case 0:
                            obj1.transform.position = pos1;
                            Manager_GameScene.activedObstacleRail = 0;
                            break;
                        case 1:
                            obj1.transform.position = pos3;
                            Manager_GameScene.activedObstacleRail = 2;
                            break;
                    }
                    break;
                case 1:
                    pos = Random.Range(0, 3);
                    GameObject obj2 = obsPool.GetObject(obs2);
                    switch (pos)
                    {
                        case 0:
                            obj2.transform.position = pos1;
                            Manager_GameScene.activedObstacleRail = 0;
                            break;
                        case 1:
                            obj2.transform.position = pos2;
                            Manager_GameScene.activedObstacleRail = 1;
                            break;
                        case 2:
                            obj2.transform.position = pos3;
                            Manager_GameScene.activedObstacleRail = 2;
                            break;
                    }
                    break;
                case 2:
                    pos = Random.Range(0, 3);
                    GameObject obj3 = obsPool.GetObject(obs3);
                    switch (pos)
                    {
                        case 0:
                            obj3.transform.position = pos1;
                            Manager_GameScene.activedObstacleRail = 0;
                            break;
                        case 1:
                            obj3.transform.position = pos2;
                            Manager_GameScene.activedObstacleRail = 1;
                            break;
                        case 2:
                            obj3.transform.position = pos3;
                            Manager_GameScene.activedObstacleRail = 2;
                            break;
                    }
                    break;
                case 3:
                    pos = 0;
                    GameObject obj4 = obsPool.GetObject(obs4);
                    obj4.transform.position = pos2;
                    Manager_GameScene.activedObstacleRail = 4;
                    break;
            }
        }
        else
        {
            deltaTIme_obstacle += Time.deltaTime;
            distance = deltaTIme_obstacle * playerSpeed;
            Manager_GameScene.activedObstacleRail = 4;
        }
    }
}
