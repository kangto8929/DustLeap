using UnityEngine;
using UnityEngine.UI;

public class DustHouch : MonoBehaviour
{
    public Camera MainCamera; // ��ġ�� ������ ���� ī�޶�
    public Image TargetImage; // ��ġ�� ������ �̹���
    public GameObject TouchImage;//��ġ ���� �̹���
    public GameObject ParticlePrefab; // ������ ��ƼŬ ������
    private GameObject CurrentParticle; // ���� Ȱ��ȭ�� ��ƼŬ

    public static DustHouch Instance;
    public int TouchCount = 0;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && TouchImage.activeSelf == true) // ��ġ �Ǵ� Ŭ�� ����
        {
            Vector3 touchPosition = Input.mousePosition; // ��ġ�� ȭ�� ��ǥ
            Vector3 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, MainCamera.nearClipPlane));

            Debug.Log($"[��ġ ����] Screen Position: {touchPosition}");
            Debug.Log($"[��ġ ����] World Position: {worldPosition}");

            // �̹��� ���θ� ��ġ�ߴ��� �˻�
            if (IsTouchingImage(touchPosition))
            {
                Debug.Log($"[�̹��� ��ġ��!] World Position: {worldPosition}");

                // ���� ��ƼŬ�� ������ ��Ȱ��ȭ
                if (CurrentParticle != null)
                {
                    Destroy(CurrentParticle); // ���� ��ƼŬ ����
                }

                // ���ο� ��ƼŬ ����
                CurrentParticle = Instantiate(ParticlePrefab, worldPosition, Quaternion.identity);
                Debug.Log("��ƼŬ ������!");

                TouchCount = 1;
                Achievement.Instance.LockObjects[0].SetActive(false);
                Achievement.Instance.Achievements[0].SetActive(true);
            }
        }
    }

    private bool IsTouchingImage(Vector2 screenPosition)
    {
        RectTransform rectTransform = TargetImage.GetComponent<RectTransform>();

        // UI ��ǥ ��ȯ
        Vector2 localPoint;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, MainCamera, out localPoint);

        if (isInside)
        {
            // �̹����� ũ�� �ȿ� �ִ��� Ȯ��
            if (localPoint.x >= -rectTransform.rect.width / 2 && localPoint.x <= rectTransform.rect.width / 2 &&
                localPoint.y >= -rectTransform.rect.height / 2 && localPoint.y <= rectTransform.rect.height / 2)
            {
                return true; // �̹��� ���� ��ġ��
            }
        }
        return false; // �̹��� �ٱ� ��ġ��
    }
}

/*using UnityEngine;
using UnityEngine.UI;

public class DustHouch : MonoBehaviour
{
    public Camera MainCamera; // ��ġ�� ������ ���� ī�޶�
    public Image TargetImage; // ��ġ�� ������ �̹���

    public GameObject ParticlePrefab; //�߰�
    private GameObject CurrentParticle; // ���� Ȱ��ȭ�� ��ƼŬ

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��ġ �Ǵ� Ŭ�� ����
        {
            Vector3 touchPosition = Input.mousePosition; // ��ġ�� ȭ�� ��ǥ
            Vector3 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, MainCamera.nearClipPlane));

            Debug.Log($"[��ġ ����] Screen Position: {touchPosition}");
            Debug.Log($"[��ġ ����] World Position: {worldPosition}");

            if (CurrentParticle != null)
            {
                CurrentParticle.SetActive(false); // ��Ȱ��ȭ
            }

            // ���ο� ��ƼŬ ����
            CurrentParticle = Instantiate(ParticlePrefab, worldPosition, Quaternion.identity);
            CurrentParticle.SetActive(true);
            //ParticlePrefab = Instantiate(ParticlePrefab, worldPosition, Quaternion.identity);
            Debug.Log("��ƼŬ ����");

        }
    }

    private bool IsTouchingImage(Vector3 worldPosition)
    {
        // �̹����� RectTransform ��������
        RectTransform rectTransform = TargetImage.GetComponent<RectTransform>();

        // ���� ��ǥ -> ���� ��ǥ ��ȯ
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, worldPosition, MainCamera, out localPoint);

        // �̹����� ũ�� �ȿ� �ִ��� Ȯ��
        if (localPoint.x >= -rectTransform.rect.width / 2 && localPoint.x <= rectTransform.rect.width / 2 &&
            localPoint.y >= -rectTransform.rect.height / 2 && localPoint.y <= rectTransform.rect.height / 2)
        {
            return true; // �̹��� ���θ� ��ġ��
        }
        return false; // �̹��� �ٱ��� ��ġ��
    }
}*/
