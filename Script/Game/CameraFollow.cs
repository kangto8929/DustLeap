using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float followThreshold = 2.5f; // 카메라가 따라가기 시작하는 Y 좌표
    public float smoothSpeed = 2f; // 카메라 이동 속도
    public Vector3 resetPosition = new Vector3(0, 0, -34); // 초기화 위치 (0, 0, -34)
    public float maxYPosition = 91.4f; // 카메라가 올라갈 수 있는 최대 Y 값

    private bool isFollowing = false; // 따라가는 상태인지 확인

    void LateUpdate()
    {
        if (player == null) return;

        if (player.position.y > followThreshold)
        {
            isFollowing = true;
        }

        Vector3 targetPosition;

        if (isFollowing)
        {
            targetPosition = new Vector3(transform.position.x, player.position.y, -34); // Z 좌표 유지
        }
        else
        {
            targetPosition = resetPosition;
        }

        // Y 좌표를 0 이상, 63.3 이하로 제한
        targetPosition.y = Mathf.Clamp(targetPosition.y, resetPosition.y, maxYPosition);

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
