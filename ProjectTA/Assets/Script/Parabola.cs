using UnityEngine;
using static System.Net.WebRequestMethods;



public class Parabola : MonoBehaviour
{
    public enum Bool
    {   FALSE = 0,
        TRUE = 1 }

    public Vector3 initialVelocity; // 초기 속도
    public Vector3 initialPosition;
    public float gravity = -9.81f; // 중력 (단위: m/s²)
    public float bounceFactor = 1f; // 튕김 계수

    private Vector3 velocity; // 현재 속도
    private float time; // 시간

    private Obstacle ModelControl;

    Bool move = Bool.FALSE;

    void Start()
    {
        // 초기 속도를 설정
        initialVelocity = new Vector3(6, 17.5f, 6);
        velocity = initialVelocity;
        velocity.y = 0;
        initialPosition = this.gameObject.transform.position;
        time = 0f;
        ModelControl = GetComponentInParent<Obstacle>();

        // Rigidbody 추가
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

    }

    void FixedUpdate()
    {
        if (ModelControl.Trigger_CAUTION)
        {
            move = Bool.TRUE;
        }

        if (move == Bool.TRUE)
        {
            time += Time.deltaTime;

            // 포물선 공식
            Vector3 displacement = velocity * time + new Vector3(0, gravity * time * time / 2, 0);
            transform.position = initialPosition + displacement;

            // 현재 속도 업데이트
            velocity += new Vector3(0, gravity * Time.deltaTime, 0);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // 바닥에 충돌했을 때 튕기는 처리
        initialPosition = this.gameObject.transform.position;

        // Y축 속도를 튕김 계수로 조정
        velocity.y = initialVelocity.y * bounceFactor;

        // 바닥에서 튕기는 위치 조정 (살짝 위로)
        transform.position += new Vector3(0, 0.1f, 0);

        // 시간 초기화
        time = 0f;
        bounceFactor -= 0.2f;
    }
}