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
    public static int line = 1;
    private Vector3 targetPos;

    private bool isGround;
    private bool isSliding;

    // ��ֹ� Collision ����
    private RaycastHit rayHit_Left; private RaycastHit rayHit_Right; private RaycastHit rayHit_Forward;
    float rayDistance = 5f;

    // ���� ������Ʈ
    private Rigidbody rb;
    private CapsuleCollider col;

    private void Awake()
    {
        InitPlayer();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()  // ��ֹ� ����
    {
        if (Physics.Raycast(transform.position, Vector3.left * rayDistance, out rayHit_Left, rayDistance))    // ����
        {
            if (rayHit_Left.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rayHit_Left.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
        }
        if (Physics.Raycast(transform.position, Vector3.right * rayDistance, out rayHit_Right, rayDistance))    // ����
        {
            if (rayHit_Right.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rayHit_Right.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out rayHit_Forward, 1))    // ����
        {
            if (rayHit_Forward.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rayHit_Forward.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
        }
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
            // todo : �����̵� ���� �� �ݶ��̴� ���� ����
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
