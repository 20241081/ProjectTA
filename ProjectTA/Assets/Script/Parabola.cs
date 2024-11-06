using UnityEngine;
using static System.Net.WebRequestMethods;



public class Parabola : MonoBehaviour
{
    public enum Bool
    {   FALSE = 0,
        TRUE = 1 }

    public Vector3 initialVelocity; // �ʱ� �ӵ�
    public Vector3 initialPosition;
    public float gravity = -9.81f; // �߷� (����: m/s��)
    public float bounceFactor = 1f; // ƨ�� ���

    private Vector3 velocity; // ���� �ӵ�
    private float time; // �ð�

    private Obstacle ModelControl;

    Bool move = Bool.FALSE;

    void Start()
    {
        // �ʱ� �ӵ��� ����
        initialVelocity = new Vector3(6, 17.5f, 6);
        velocity = initialVelocity;
        velocity.y = 0;
        initialPosition = this.gameObject.transform.position;
        time = 0f;
        ModelControl = GetComponentInParent<Obstacle>();

        // Rigidbody �߰�
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

            // ������ ����
            Vector3 displacement = velocity * time + new Vector3(0, gravity * time * time / 2, 0);
            transform.position = initialPosition + displacement;

            // ���� �ӵ� ������Ʈ
            velocity += new Vector3(0, gravity * Time.deltaTime, 0);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // �ٴڿ� �浹���� �� ƨ��� ó��
        initialPosition = this.gameObject.transform.position;

        // Y�� �ӵ��� ƨ�� ����� ����
        velocity.y = initialVelocity.y * bounceFactor;

        // �ٴڿ��� ƨ��� ��ġ ���� (��¦ ����)
        transform.position += new Vector3(0, 0.1f, 0);

        // �ð� �ʱ�ȭ
        time = 0f;
        bounceFactor -= 0.2f;
    }
}