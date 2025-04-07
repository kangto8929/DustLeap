using UnityEngine;

public class Black_FootHold : MonoBehaviour
{
    [SerializeField] private GameObject[] Platform; // 모든 플랫폼 오브젝트
    [SerializeField] private Transform _playerTransform; // 플레이어의 Transform
    private float lastPlayerY; // 이전 프레임의 플레이어 Y 위치

    private void Start()
    {
        // "Platform" 태그를 가진 모든 오브젝트 찾기
        Platform = GameObject.FindGameObjectsWithTag("Platform");

        // 플레이어 트랜스폼 확인
        if (_playerTransform == null)
        {
            Debug.LogError(" 플레이어 트랜스폼이 할당되지 않았습니다!");
            return;
        }

        lastPlayerY = _playerTransform.position.y;

        Debug.Log(" 플랫폼 개수: " + Platform.Length);
    }

    private void Update() //  `FixedUpdate()` → `Update()`로 변경
    {
        if (_playerTransform == null) return; // 플레이어가 없으면 실행 안 함

        float playerY = _playerTransform.position.y; // 현재 플레이어 Y 위치

        // 플레이어의 Y 위치가 변경되었을 때만 실행
        if (Mathf.Abs(playerY - lastPlayerY) < 0.01f)
        {
            return; // 위치 변화가 거의 없으면 실행 안 함
        }

        foreach (GameObject platform in Platform)
        {
            Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();
            Collider2D platformCollider = platform.GetComponent<Collider2D>();

            if (platformRigidBody == null || platformCollider == null) continue; // Rigidbody2D나 Collider2D가 없으면 스킵

            // 플레이어가 해당 플랫폼보다 최소 1 이상 위에 있으면 "Player" 레이어 제외하지 않고 트리거 비활성화
            bool shouldExcludePlayerLayer = platform.transform.position.y + 0.5f < playerY;

            // `excludeLayers` 설정: 플레이어보다 위에 있으면 Nothing, 아래에 있으면 "Player" 제외
            if (shouldExcludePlayerLayer)
            {
                platformRigidBody.excludeLayers = 0; // "Player" 레이어 제외하지 않음 (Nothing 상태)
                platformCollider.isTrigger = false; // 트리거 비활성화
            }
            else
            {
                platformRigidBody.excludeLayers = LayerMask.GetMask("Player"); // "Player" 레이어 제외
                platformCollider.isTrigger = true; // 트리거 활성화
            }
        }

        // 마지막 플레이어 위치 업데이트
        lastPlayerY = playerY;
    }
}

/*using UnityEngine;

public class Black_FootHold : MonoBehaviour
{
    [SerializeField] private GameObject[] Platform; // 모든 플랫폼 오브젝트
    [SerializeField] private Transform _playerTransform; // 플레이어의 Transform
    private float lastPlayerY; // 이전 프레임의 플레이어 Y 위치

    public static Black_FootHold Instance;

    private void Start()
    {
        Instance = this;

        // "Platform" 태그를 가진 모든 오브젝트 찾기
        Platform = GameObject.FindGameObjectsWithTag("Platform");

        // 플레이어 트랜스폼 확인
        if (_playerTransform == null)
        {
            Debug.LogError("플레이어 트랜스폼이 할당되지 않았습니다!");
            return;
        }

        lastPlayerY = _playerTransform.position.y;

        Debug.Log("플랫폼 개수: " + Platform.Length);
    }

    public void SetPlatformActive()//부딪히지 않음
    {
        // 플랫폼을 활성화 상태로 설정
        foreach (GameObject platform in Platform)
        {
            Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();
            Collider2D platformCollider = platform.GetComponent<Collider2D>();

            if (platformRigidBody == null || platformCollider == null) continue; // Rigidbody2D나 Collider2D가 없으면 스킵

            platformRigidBody.excludeLayers = LayerMask.GetMask("Player"); // "Player" 레이어 제외
            platformCollider.isTrigger = true; // 트리거 활성화
        }
    }

    public void SetPlatformInactive()//부딪힘
    {
        // 플랫폼을 비활성화 상태로 설정
        foreach (GameObject platform in Platform)
        {
            Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();
            Collider2D platformCollider = platform.GetComponent<Collider2D>();

            if (platformRigidBody == null || platformCollider == null) continue; // Rigidbody2D나 Collider2D가 없으면 스킵

            platformRigidBody.excludeLayers = 0; // "Player" 레이어 제외하지 않음 (Nothing 상태)
            platformCollider.isTrigger = false; // 트리거 비활성화
        }
    }

}*/




/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class Black_FootHold : MonoBehaviour
{
    //플레이어에 넣을 스크립트
    public GameObject[] _platform;
    public LayerMask HitLayer;//감지할 레이어 설정

    public GameObject ScanObject;

    private Collider2D playerCollider;

    //25.03.28.수정
    //레이저를 쏴서 위에 있는 거 찾기
    //위에 있는 콜라이더 및 RigidBody2D비활성
    //나머지 것들은 콜라이더 활성

    //플레이어 레이캐스트 관련


    private void Start()
    {
        //플레이어 콜라이더
        playerCollider = GetComponent<Collider2D>();

        //플랫폼 태그 있는거 모두 찾음
        _platform = GameObject.FindGameObjectsWithTag("Platform");

        //플랫폼 개수 몇 개인지
        Debug.Log("플랫폼 개수: " + _platform.Length);
    }

    private void FixedUpdate()
    {
        //레이캐스트 위로 발사
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.9f, HitLayer);
        //여기서 5는 길이

        if(hit.collider != null)
        {
            ScanObject = hit.collider.gameObject;
            Debug.Log("레이저가" + hit.collider.gameObject.name);

            Rigidbody2D scanObjectRigidBody2D = ScanObject.GetComponent<Rigidbody2D>();

            if(scanObjectRigidBody2D != null)
            {
                scanObjectRigidBody2D.excludeLayers = LayerMask.GetMask("Player");
            }
        }
        if (hit.collider == null)
        {
            // 레이캐스트가 아무것도 충돌하지 않았을 때 실행되는 코드
            for (int i = 0; i < _platform.Length; i++)
            {
                // 플랫폼 오브젝트들을 하나씩 반복
                Rigidbody2D platformRigidBody = _platform[i].GetComponent<Rigidbody2D>();

                if (platformRigidBody != null)
                {
                    // 만약 플랫폼 오브젝트에 Rigidbody2D가 있다면
                    platformRigidBody.excludeLayers = 0;  // excludeLayers를 0으로 설정
                                                          // '0'을 설정하는 것은 해당 Rigidbody2D가 충돌을 무시하는 상태로 만드는 것
                }
            }
        }



        Debug.DrawRay(transform.position, Vector2.up * 0.9f, Color.green);

       // updateCollision();//스캔한거 충돌 비활성, 나머지 정상 충돌

        foreach (GameObject platform in _platform)
        {
            if(platform != ScanObject)
            {
                Rigidbody2D rigidbody2d = platform.GetComponent<Rigidbody2D>();
                if(rigidbody2d != null)
                {
                    rigidbody2d.excludeLayers = 0;
                }
            }
        }
    }

}*/
