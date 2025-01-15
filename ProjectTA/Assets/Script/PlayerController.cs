using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // PlayerMove ���� ����
    public float moveSpeed;
    public float sideSpeed = 6.0f;
    public float jumpForce = 6.0f;

    public float lineDistance = 6.0f;
    private int line = 1;
    private Vector3 targetPos;

    private bool isGround;
    private bool isSliding;

    // ���� ������Ʈ
    private Rigidbody rb;
    private CapsuleCollider col;

    private void Awake()
    {
        InitPlayer();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void InitPlayer()
    {
        isGround = true;
        isSliding = false;

        moveSpeed = 10f;
        line = 1;
        targetPos = transform.position;
    }

    private void PlayerMove()
    {
        // �÷��̾� �̵�
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        // �÷��̾� �¿� �̵�
        if (Input.GetKeyDown(KeyCode.A) && line > 0)
        {
            line--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && line < 2)
        {
            line++;
        }

        targetPos = new Vector3(line * lineDistance - lineDistance,
                                transform.position.y,
                                transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * sideSpeed);

        // �÷��̾� ���� �� �����̵�
        if (isGround)
        {
            if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.Space))
            {
                isGround = false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if(!isSliding && Input.GetKeyDown(KeyCode.S))
        {
            isSliding = false;
            Debug.Log("�����̵�");
            // todo : �����̵� ���� �� �ݶ��̴� ���� ����1
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
