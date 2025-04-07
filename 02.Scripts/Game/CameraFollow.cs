using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float followThreshold = 2.5f; // ī�޶� ���󰡱� �����ϴ� Y ��ǥ
    public float smoothSpeed = 2f; // ī�޶� �̵� �ӵ�
    public Vector3 resetPosition = new Vector3(0, 0, -34); // �ʱ�ȭ ��ġ (0, 0, -34)
    public float maxYPosition = 91.4f; // ī�޶� �ö� �� �ִ� �ִ� Y ��

    private bool isFollowing = false; // ���󰡴� �������� Ȯ��

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
            targetPosition = new Vector3(transform.position.x, player.position.y, -34); // Z ��ǥ ����
        }
        else
        {
            targetPosition = resetPosition;
        }

        // Y ��ǥ�� 0 �̻�, 63.3 ���Ϸ� ����
        targetPosition.y = Mathf.Clamp(targetPosition.y, resetPosition.y, maxYPosition);

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
