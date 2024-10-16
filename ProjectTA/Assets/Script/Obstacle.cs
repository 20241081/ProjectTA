using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int TargetRail;  //������ ���� (�������� 0)
    public int ObstacleType;    //��ֹ� ���� (1~4)
    public bool Trigger_CAUTION = false;    //������ �浹
    public float DelayTime = 2.0f;  //��ֹ��� ������� �����Ҷ����� �ð�
    Vector3 TargetPos;

    public GameObject CAUTIONEffect;    //��� ����Ʈ
    public GameObject Model;    //��ֹ� ��
    GameObject Player;   //�÷��̾�

    private void Start()
    {
        TargetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Player = GameObject.Find("Player");
    }
    void FixedUpdate()
    {
        if (Trigger_CAUTION)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;    //�ߺ� ���� ����
            Obstacle_Apia(TargetRail);  // ��ֹ� ����
        }
    }

    private void Obstacle_Apia(int Rail)
    {
        Destroy(CAUTIONEffect); //��� ����

        if (Model.transform.position == TargetPos)  // ��ֹ��� ��ġ�� ����
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
                    Player.GetComponentInParent<PlayerController>().moveSpeed *= 0.3f;  //����
                    Trigger_CAUTION = false;
                    if (ObstacleType == 1)
                        StartCoroutine(Disapia_Obs01());   //��ֹ� �������
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
