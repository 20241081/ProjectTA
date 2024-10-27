using UnityEngine;

public class ParabolicMotion : MonoBehaviour
{
    public Vector3 initialVelocity; // 초기 속도
    public Vector3 initialPosition;
    public float gravity = -9.81f; // 중력 (단위: m/s²)
    public float bounceFactor = 0.7f; // 튕김 계수

    private Vector3 velocity; // 현재 속도
    private float time; // 시간

    void Start()
    {
        // 초기 속도를 설정
        velocity = initialVelocity;
        initialPosition = this.gameObject.transform.position;
        time = 0f;

        // Rigidbody 추가
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        // 중력 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Rigidbody의 중력 사용 안 함
    }

    void Update()
    {
        // 시간 증가
        time += Time.deltaTime;

        // 포물선 운동 공식을 적용
        Vector3 displacement = velocity * time + new Vector3(0, gravity * time * time / 2, 0);
        transform.position = initialPosition + displacement;

        // 현재 속도 업데이트
        velocity += new Vector3(0, gravity * Time.deltaTime, 0);

        // 땅에 닿았을 때의 처리
        /*if (transform.position.y < 0)
        {
            // 오브젝트가 땅에 닿으면 속도 초기화 또는 오브젝트 삭제 등 처리
            Destroy(gameObject); // 예: 오브젝트 삭제
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        initialPosition = this.gameObject.transform.position;

        // 바닥에 충돌했을 때 튕기는 처리
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Y축 속도를 튕김 계수로 조정
            velocity.y = -velocity.y * bounceFactor;

            // 바닥에서 튕기는 위치 조정 (살짝 위로)
            transform.position += new Vector3(0, 0.1f, 0);

            // 시간 초기화
            time = 0f;
        }
    }
}