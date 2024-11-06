using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int TargetRail;  // ������ ���� (�������� 0)
    public int ObstacleType;    // ��ֹ� ���� (1~4)
    public bool Trigger_CAUTION;    // ������ �浹
    public bool Trigger_DAMAGE;     // ��ֹ� �浹
    public float DelayTime = 2.0f;  // ��ֹ��� ������� �����Ҷ����� �ð�
    private Vector3 TargetPos;

    public GameObject CAUTIONEffect;    // ��� ����Ʈ
    public GameObject Model;    // ��ֹ� ��
    private GameObject Player;   // �÷��̾�
    private PlayerController PlayerController;
    private BoxCollider boxCollider;

    private const float ObstacleSpeed = 20f;    // ��ֹ� �̵��ӵ�

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
            boxCollider.enabled = false;    // �ߺ� ���� ����
            Obstacle_Apia(TargetRail);  // ��ֹ� ����
        }
    }
    private void Obstacle_Apia(int Rail)
    {
        if (CAUTIONEffect != null)
            Destroy(CAUTIONEffect); // ��� ����

        if (Model.transform.position != TargetPos && ObstacleType != 4)  // ��ֹ��� �̵���
        {
            Model.transform.position = Vector3.Lerp(Model.transform.position, TargetPos, Time.deltaTime * ObstacleSpeed);
        }

        if (Trigger_DAMAGE)  // �浹��
        {
            Manager_GameScene.PlayerHP--;
            PlayerController.moveSpeed *= 0.3f;  // ����
            Trigger_CAUTION = false;
            Trigger_DAMAGE = false;

            if (ObstacleType == 1)
                StartCoroutine(Disapia_Obs01());   // ��ֹ� �������
        }
    }

    IEnumerator Disapia_Obs01() // �浹�� �����
    {
        yield return StartCoroutine(Delay(DelayTime));
        Destroy(gameObject);
    }
    IEnumerator Delay(float D_Time)
    {
        yield return new WaitForSeconds (D_Time);
    }
}
