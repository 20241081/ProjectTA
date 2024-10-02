using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static int CurrentRail;  //플레이어 현재 레일. 좌측부터 1,2,3
    private RaycastHit rayHit_Left; private RaycastHit rayHit_Right; private RaycastHit rayHit_Forward;
    float rayDistance = 5f;
    void Start()
    {
        CurrentRail = 2;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CurrentRail != 1)
            {
                CurrentRail -= 1;
            }
        }   // A 입력시 CurrentRail 갱신
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (CurrentRail != 3)
            {
                CurrentRail += 1;
            }
        }   // B 입력시 CurrentRail 갱신

        Debug.DrawRay(transform.position, Vector3.left * rayDistance, Color.blue);
        Debug.DrawRay(transform.position, Vector3.right * rayDistance, Color.cyan);
        Debug.DrawRay(transform.position, Vector3.forward * 1, Color.green);
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.left*rayDistance, out rayHit_Left, rayDistance))    // 좌측
        {
            if(rayHit_Left.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rayHit_Left.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
        }
        if (Physics.Raycast(transform.position, Vector3.right * rayDistance, out rayHit_Right, rayDistance))    // 우측
        {
            if (rayHit_Right.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rayHit_Right.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out rayHit_Forward, 1))    // 정면
        {
            if (rayHit_Forward.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rayHit_Forward.transform.GetComponent<Obstacle>().Trigger_CAUTION = true;
        }
    }
}
