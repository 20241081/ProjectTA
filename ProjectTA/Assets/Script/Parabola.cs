using UnityEngine;

public class ParabolicMotion : MonoBehaviour
{
    public Vector3 initialVelocity; // �ʱ� �ӵ�
    public Vector3 initialPosition;
    public float gravity = -9.81f; // �߷� (����: m/s��)
    public float bounceFactor = 0.7f; // ƨ�� ���

    private Vector3 velocity; // ���� �ӵ�
    private float time; // �ð�

    void Start()
    {
        // �ʱ� �ӵ��� ����
        velocity = initialVelocity;
        initialPosition = this.gameObject.transform.position;
        time = 0f;

        // Rigidbody �߰�
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        // �߷� ����
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Rigidbody�� �߷� ��� �� ��
    }

    void Update()
    {
        // �ð� ����
        time += Time.deltaTime;

        // ������ � ������ ����
        Vector3 displacement = velocity * time + new Vector3(0, gravity * time * time / 2, 0);
        transform.position = initialPosition + displacement;

        // ���� �ӵ� ������Ʈ
        velocity += new Vector3(0, gravity * Time.deltaTime, 0);

        // ���� ����� ���� ó��
        /*if (transform.position.y < 0)
        {
            // ������Ʈ�� ���� ������ �ӵ� �ʱ�ȭ �Ǵ� ������Ʈ ���� �� ó��
            Destroy(gameObject); // ��: ������Ʈ ����
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        initialPosition = this.gameObject.transform.position;

        // �ٴڿ� �浹���� �� ƨ��� ó��
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Y�� �ӵ��� ƨ�� ����� ����
            velocity.y = -velocity.y * bounceFactor;

            // �ٴڿ��� ƨ��� ��ġ ���� (��¦ ����)
            transform.position += new Vector3(0, 0.1f, 0);

            // �ð� �ʱ�ȭ
            time = 0f;
        }
    }
}