using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int TargetRail;  //등장할 레일 (좌측부터 0)
    public int ObstacleType;    //장애물 종류 (1~4)
    public bool Trigger_CAUTION = false;    //경고범위 충돌
    public float DelayTime = 2.0f;  //장애물이 사라지기 시작할때까지 시간
    Vector3 TargetPos;

    public GameObject CAUTIONEffect;    //경고 이펙트
    public GameObject Model;    //장애물 모델
    GameObject Player;   //플레이어

    private void Start()
    {
        TargetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Player = GameObject.Find("Player");
    }
    void FixedUpdate()
    {
        if (Trigger_CAUTION)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;    //중복 감지 방지
            Obstacle_Apia(TargetRail);  // 장애물 등장
        }
    }

    private void Obstacle_Apia(int Rail)
    {
        Destroy(CAUTIONEffect); //경고 제거

        if (Model.transform.position == TargetPos)  // 장애물이 위치에 도달
        {
            if (PlayerController.line == Rail)
            {
                if (Player.transform.position.z >= transform.position.z + 1)
                {
                    Trigger_CAUTION = false;
                }
                else if (Trigger_CAUTION && Player.transform.position.z > transform.position.z - 1)
                {
                    Manager_GameScene.PlayerHP--;
                    Player.GetComponentInParent<PlayerController>().moveSpeed *= 0.3f;  //감속
                    Trigger_CAUTION = false;
                    if (ObstacleType == 1)
                        StartCoroutine(Disapia_Obs01());   //장애물 사라지기
                    else
                        return;
                }
            }
        }
        else
        {
            Model.transform.position = Vector3.Lerp(Model.transform.position, TargetPos, Time.deltaTime * 20f);
        }
    }

    IEnumerator Disapia_Obs01()
    {
        yield return StartCoroutine(Delay(DelayTime));
        Destroy(gameObject);
    }
    IEnumerator Delay(float D_Time)
    {
        yield return new WaitForSeconds (D_Time);
    }
}
