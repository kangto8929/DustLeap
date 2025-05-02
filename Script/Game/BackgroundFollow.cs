using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform Player; // �÷��̾�
    public float ParallaxFactor = 0.1f; // ��� ������ ���� (�������� �� ������)

    private float startY;

    void Start()
    {
        if (Player != null)
        {
            startY = transform.position.y;
        }
    }

    void Update()
    {
        if (Player != null)
        {
            float targetY = startY + (Player.position.y * ParallaxFactor);
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
}