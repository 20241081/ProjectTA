using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int TargetRail;  // 등장할 레일 (좌측부터 0)
    public int ObstacleType;    // 장애물 종류 (1~4)
    public bool Trigger_CAUTION;    // 경고범위 충돌
    public bool Trigger_DAMAGE;     // 장애물 충돌
    public float DelayTime = 2.0f;  // 장애물이 사라지기 시작할때까지 시간
    private Vector3 TargetPos;

    public GameObject CAUTIONEffect;    // 경고 이펙트
    public GameObject Model;    // 장애물 모델
    private GameObject Player;   // 플레이어
    private PlayerController PlayerController;
    private BoxCollider boxCollider;

    private const float ObstacleSpeed = 20f;    // 장애물 이동속도

    private void Start()
    {
        TargetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Player = GameObject.Find("Player");
        Trigger_CAUTION = false;
        PlayerController = Player.GetComponentInParent<PlayerController>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (Trigger_CAUTION)
        {
            boxCollider.enabled = false;    // 중복 감지 방지
            Obstacle_Apia(TargetRail);  // 장애물 등장
        }
    }
    private void Obstacle_Apia(int Rail)
    {
        if (CAUTIONEffect != null)
            Destroy(CAUTIONEffect); // 경고 제거

        if (Model.transform.position != TargetPos && ObstacleType != 4)  // 장애물이 이동중
        {
            Model.transform.position = Vector3.Lerp(Model.transform.position, TargetPos, Time.deltaTime * ObstacleSpeed);
        }

        if (Trigger_DAMAGE)  // 충돌시
        {
            Manager_GameScene.PlayerHP--;
            PlayerController.moveSpeed *= 0.3f;  // 감속
            Trigger_CAUTION = false;
            Trigger_DAMAGE = false;

            if (ObstacleType == 1)
                StartCoroutine(Disapia_Obs01());   // 장애물 사라지기
        }
    }

    IEnumerator Disapia_Obs01() // 충돌후 사라짐
    {
        yield return StartCoroutine(Delay(DelayTime));
        Destroy(gameObject);
    }
    IEnumerator Delay(float D_Time)
    {
        yield return new WaitForSeconds (D_Time);
    }
}
