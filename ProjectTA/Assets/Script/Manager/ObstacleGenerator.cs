using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject Obstacle01;
    public GameObject Obstacle02;

    public GameObject Player;

    public float delta = 0;
    public float standardTime = 2.0f;  // 장애물 생성까지 시간

    int Gen_Type = 0;   // 생성할 장애물 유형
    int Gen_Rail = 0;   // 생성할 장애물이 등장할 레일

    void Update()
    {
        delta += Time.deltaTime;
        if (delta > standardTime)
        {
            delta = 0;
            Gen_Rail = Random.Range(-1, 2);    // -1~1
            switch (Gen_Rail)
            {
                case 0:
                    Gen_Type = Random.Range(2, 3);   //2~2
                    break;
                default:
                    Gen_Type = Random.Range(1, 3);   //1~2
                    break;
            }
            
            
            Create_Obstacle(Gen_Type, Gen_Rail);
            Debug.Log("장애물 생성");
        }
    }

    void Create_Obstacle(int Type, int Rail)
    {
        GameObject gameObstacle;
        
        float PositionZ = Player.transform.position.z + 15f;
        float PositionX = Rail * 6;
        switch (Type)
        {
            case 1:
                gameObstacle = Instantiate(Obstacle01) as GameObject;
                gameObstacle.transform.GetComponent<Obstacle>().ObstacleType = Type;
                gameObstacle.transform.GetComponent<Obstacle>().TargetRail = Rail + 1;
                gameObstacle.transform.position = new Vector3(PositionX, 4, PositionZ);
                if (Rail == 1)
                {
                    gameObstacle.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                break;
            case 2:
                gameObstacle = Instantiate(Obstacle02) as GameObject;
                gameObstacle.transform.GetComponent<Obstacle>().ObstacleType = Type;
                gameObstacle.transform.GetComponent<Obstacle>().TargetRail = Rail + 1;
                gameObstacle.transform.position = new Vector3(PositionX, 4, PositionZ);
                break;
        }
    }
}
