using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5;
    public GameObject mainCamera;

    private void Awake()
    {
        mainCamera.transform.localPosition = new Vector3(0,7.5f,-1);
        mainCamera.transform.localRotation = Quaternion.Euler(20,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
    }
}
