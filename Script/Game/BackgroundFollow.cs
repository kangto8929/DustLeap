using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform Player; // 플레이어
    public float ParallaxFactor = 0.1f; // 배경 움직임 정도 (낮을수록 덜 움직임)

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