using UnityEngine;
using UnityEngine.UI;

public class DustHouch : MonoBehaviour
{
    public Camera MainCamera; // 터치를 감지할 메인 카메라
    public Image TargetImage; // 터치를 감지할 이미지
    public GameObject TouchImage;//터치 감지 이미지
    public GameObject ParticlePrefab; // 생성할 파티클 프리팹
    private GameObject CurrentParticle; // 현재 활성화된 파티클

    public static DustHouch Instance;
    public int TouchCount = 0;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && TouchImage.activeSelf == true) // 터치 또는 클릭 감지
        {
            Vector3 touchPosition = Input.mousePosition; // 터치한 화면 좌표
            Vector3 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, MainCamera.nearClipPlane));

            Debug.Log($"[터치 감지] Screen Position: {touchPosition}");
            Debug.Log($"[터치 감지] World Position: {worldPosition}");

            // 이미지 내부를 터치했는지 검사
            if (IsTouchingImage(touchPosition))
            {
                Debug.Log($"[이미지 터치됨!] World Position: {worldPosition}");

                // 기존 파티클이 있으면 비활성화
                if (CurrentParticle != null)
                {
                    Destroy(CurrentParticle); // 기존 파티클 삭제
                }

                // 새로운 파티클 생성
                CurrentParticle = Instantiate(ParticlePrefab, worldPosition, Quaternion.identity);
                Debug.Log("파티클 생성됨!");

                TouchCount = 1;
                Achievement.Instance.LockObjects[0].SetActive(false);
                Achievement.Instance.Achievements[0].SetActive(true);
            }
        }
    }

    private bool IsTouchingImage(Vector2 screenPosition)
    {
        RectTransform rectTransform = TargetImage.GetComponent<RectTransform>();

        // UI 좌표 변환
        Vector2 localPoint;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, MainCamera, out localPoint);

        if (isInside)
        {
            // 이미지의 크기 안에 있는지 확인
            if (localPoint.x >= -rectTransform.rect.width / 2 && localPoint.x <= rectTransform.rect.width / 2 &&
                localPoint.y >= -rectTransform.rect.height / 2 && localPoint.y <= rectTransform.rect.height / 2)
            {
                return true; // 이미지 내부 터치됨
            }
        }
        return false; // 이미지 바깥 터치됨
    }
}

/*using UnityEngine;
using UnityEngine.UI;

public class DustHouch : MonoBehaviour
{
    public Camera MainCamera; // 터치를 감지할 메인 카메라
    public Image TargetImage; // 터치를 감지할 이미지

    public GameObject ParticlePrefab; //추가
    private GameObject CurrentParticle; // 현재 활성화된 파티클

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 터치 또는 클릭 감지
        {
            Vector3 touchPosition = Input.mousePosition; // 터치한 화면 좌표
            Vector3 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, MainCamera.nearClipPlane));

            Debug.Log($"[터치 감지] Screen Position: {touchPosition}");
            Debug.Log($"[터치 감지] World Position: {worldPosition}");

            if (CurrentParticle != null)
            {
                CurrentParticle.SetActive(false); // 비활성화
            }

            // 새로운 파티클 생성
            CurrentParticle = Instantiate(ParticlePrefab, worldPosition, Quaternion.identity);
            CurrentParticle.SetActive(true);
            //ParticlePrefab = Instantiate(ParticlePrefab, worldPosition, Quaternion.identity);
            Debug.Log("파티클 나와");

        }
    }

    private bool IsTouchingImage(Vector3 worldPosition)
    {
        // 이미지의 RectTransform 가져오기
        RectTransform rectTransform = TargetImage.GetComponent<RectTransform>();

        // 월드 좌표 -> 로컬 좌표 변환
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, worldPosition, MainCamera, out localPoint);

        // 이미지의 크기 안에 있는지 확인
        if (localPoint.x >= -rectTransform.rect.width / 2 && localPoint.x <= rectTransform.rect.width / 2 &&
            localPoint.y >= -rectTransform.rect.height / 2 && localPoint.y <= rectTransform.rect.height / 2)
        {
            return true; // 이미지 내부를 터치함
        }
        return false; // 이미지 바깥을 터치함
    }
}*/
