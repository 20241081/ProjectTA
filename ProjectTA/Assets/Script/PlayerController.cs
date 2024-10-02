using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float sideSpeed = 6.0f;

    Transform tf;   /*이후 주석처리할 것*/

    private void Start()
    {
        tf = transform; /*이후 주석처리할 것*/
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        
        //todo : 1번째 줄일때의 A키 입력 제한, 3번째 줄일때의 D키 입력 제한

        if (Input.GetKeyDown(KeyCode.A))
        {
            //transform.Translate(Vector3.left * sideSpeed * Time.deltaTime, Space.World);
            tf.position = new Vector3(tf.position.x - 6, tf.position.y, tf.position.z); /*이후 주석처리할 것*/
            //Debug.Log("A");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //transform.Translate(Vector3.right * sideSpeed * Time.deltaTime, Space.World);
            tf.position = new Vector3(tf.position.x + 6, tf.position.y, tf.position.z); /*이후 주석처리할 것*/
            //Debug.Log("D");
        }
    }
}
