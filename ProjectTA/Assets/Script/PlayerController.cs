using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // PlayerMove 구현 변수
    public float moveSpeed;
    public float sideSpeed = 6.0f;
    public float jumpForce = 6.0f;

    public float lineDistance = 6.0f;
    public static int line = 1;
    public int LINE { 
        get => line;
    }
    private Vector3 targetPos;

    private bool isGround;
    private bool isSliding;

    // 장애물 Collision 변수
    private RaycastHit rayHit_Left; private RaycastHit rayHit_Right; private RaycastHit rayHit_Forward; private RaycastHit rayHit_Up;
    float rayDistance = 1f;

    // 참조 컴포넌트
    private Rigidbody rb;
    private CapsuleCollider col;

    private void Awake()
    {
        InitPlayer();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()  // 장애물 감지
    {
        if (Physics.Raycast(transform.position, Vector3.left * rayDistance, out rayHit_Left, rayDistance))    // 좌측
        {
            if (rayHit_Left.collider.CompareTag("Obstacle"))
                rayHit_Left.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
            else if (rayHit_Left.collider.CompareTag("Obstacle_Model"))
                rayHit_Left.transform.parent.GetComponent<Obstacle>().Trigger_DAMAGE = true;
        }
        else if (Physics.Raycast(transform.position, Vector3.right * rayDistance, out rayHit_Right, rayDistance))    // 우측
        {
            if (rayHit_Right.collider.CompareTag("Obstacle"))
                rayHit_Right.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
            else if (rayHit_Right.collider.CompareTag("Obstacle_Model"))
                rayHit_Right.transform.parent.GetComponent<Obstacle>().Trigger_DAMAGE = true;
        }
        else if (Physics.Raycast(transform.position, Vector3.forward, out rayHit_Forward, 1))    // 정면
        {
            if (rayHit_Forward.collider.CompareTag("Obstacle"))
                rayHit_Forward.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
            else if (rayHit_Forward.collider.CompareTag("Obstacle_Model"))
                rayHit_Forward.transform.parent.GetComponent<Obstacle>().Trigger_DAMAGE = true;
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out rayHit_Up, 1))    // 정면
        {
            if (rayHit_Up.collider.CompareTag("Obstacle"))
                rayHit_Up.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
            else if (rayHit_Up.collider.CompareTag("Obstacle_Model"))
                rayHit_Up.transform.parent.GetComponent<Obstacle>().Trigger_DAMAGE = true;
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

        moveSpeed = 16f;
        line = 1;
        targetPos = transform.position;
    }

    private void PlayerMove()
    {
        // 플레이어 이동
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        // 플레이어 좌우 이동
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

        // 플레이어 점프 및 슬라이딩
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
            // todo : 슬라이딩 구현 및 콜라이더 범위 수정
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
