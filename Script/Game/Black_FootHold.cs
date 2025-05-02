using UnityEngine;

public class Black_FootHold : MonoBehaviour
{
    [SerializeField] private GameObject[] Platform; // ��� �÷��� ������Ʈ
    [SerializeField] private Transform _playerTransform; // �÷��̾��� Transform
    private float lastPlayerY; // ���� �������� �÷��̾� Y ��ġ

    private void Start()
    {
        // "Platform" �±׸� ���� ��� ������Ʈ ã��
        Platform = GameObject.FindGameObjectsWithTag("Platform");

        // �÷��̾� Ʈ������ Ȯ��
        if (_playerTransform == null)
        {
            Debug.LogError(" �÷��̾� Ʈ�������� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        lastPlayerY = _playerTransform.position.y;

        Debug.Log(" �÷��� ����: " + Platform.Length);
    }

    private void Update() //  `FixedUpdate()` �� `Update()`�� ����
    {
        if (_playerTransform == null) return; // �÷��̾ ������ ���� �� ��

        float playerY = _playerTransform.position.y; // ���� �÷��̾� Y ��ġ

        // �÷��̾��� Y ��ġ�� ����Ǿ��� ���� ����
        if (Mathf.Abs(playerY - lastPlayerY) < 0.01f)
        {
            return; // ��ġ ��ȭ�� ���� ������ ���� �� ��
        }

        foreach (GameObject platform in Platform)
        {
            Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();
            Collider2D platformCollider = platform.GetComponent<Collider2D>();

            if (platformRigidBody == null || platformCollider == null) continue; // Rigidbody2D�� Collider2D�� ������ ��ŵ

            // �÷��̾ �ش� �÷������� �ּ� 1 �̻� ���� ������ "Player" ���̾� �������� �ʰ� Ʈ���� ��Ȱ��ȭ
            bool shouldExcludePlayerLayer = platform.transform.position.y + 0.5f < playerY;

            // `excludeLayers` ����: �÷��̾�� ���� ������ Nothing, �Ʒ��� ������ "Player" ����
            if (shouldExcludePlayerLayer)
            {
                platformRigidBody.excludeLayers = 0; // "Player" ���̾� �������� ���� (Nothing ����)
                platformCollider.isTrigger = false; // Ʈ���� ��Ȱ��ȭ
            }
            else
            {
                platformRigidBody.excludeLayers = LayerMask.GetMask("Player"); // "Player" ���̾� ����
                platformCollider.isTrigger = true; // Ʈ���� Ȱ��ȭ
            }
        }

        // ������ �÷��̾� ��ġ ������Ʈ
        lastPlayerY = playerY;
    }
}

/*using UnityEngine;

public class Black_FootHold : MonoBehaviour
{
    [SerializeField] private GameObject[] Platform; // ��� �÷��� ������Ʈ
    [SerializeField] private Transform _playerTransform; // �÷��̾��� Transform
    private float lastPlayerY; // ���� �������� �÷��̾� Y ��ġ

    public static Black_FootHold Instance;

    private void Start()
    {
        Instance = this;

        // "Platform" �±׸� ���� ��� ������Ʈ ã��
        Platform = GameObject.FindGameObjectsWithTag("Platform");

        // �÷��̾� Ʈ������ Ȯ��
        if (_playerTransform == null)
        {
            Debug.LogError("�÷��̾� Ʈ�������� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        lastPlayerY = _playerTransform.position.y;

        Debug.Log("�÷��� ����: " + Platform.Length);
    }

    public void SetPlatformActive()//�ε����� ����
    {
        // �÷����� Ȱ��ȭ ���·� ����
        foreach (GameObject platform in Platform)
        {
            Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();
            Collider2D platformCollider = platform.GetComponent<Collider2D>();

            if (platformRigidBody == null || platformCollider == null) continue; // Rigidbody2D�� Collider2D�� ������ ��ŵ

            platformRigidBody.excludeLayers = LayerMask.GetMask("Player"); // "Player" ���̾� ����
            platformCollider.isTrigger = true; // Ʈ���� Ȱ��ȭ
        }
    }

    public void SetPlatformInactive()//�ε���
    {
        // �÷����� ��Ȱ��ȭ ���·� ����
        foreach (GameObject platform in Platform)
        {
            Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();
            Collider2D platformCollider = platform.GetComponent<Collider2D>();

            if (platformRigidBody == null || platformCollider == null) continue; // Rigidbody2D�� Collider2D�� ������ ��ŵ

            platformRigidBody.excludeLayers = 0; // "Player" ���̾� �������� ���� (Nothing ����)
            platformCollider.isTrigger = false; // Ʈ���� ��Ȱ��ȭ
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
    //�÷��̾ ���� ��ũ��Ʈ
    public GameObject[] _platform;
    public LayerMask HitLayer;//������ ���̾� ����

    public GameObject ScanObject;

    private Collider2D playerCollider;

    //25.03.28.����
    //�������� ���� ���� �ִ� �� ã��
    //���� �ִ� �ݶ��̴� �� RigidBody2D��Ȱ��
    //������ �͵��� �ݶ��̴� Ȱ��

    //�÷��̾� ����ĳ��Ʈ ����


    private void Start()
    {
        //�÷��̾� �ݶ��̴�
        playerCollider = GetComponent<Collider2D>();

        //�÷��� �±� �ִ°� ��� ã��
        _platform = GameObject.FindGameObjectsWithTag("Platform");

        //�÷��� ���� �� ������
        Debug.Log("�÷��� ����: " + _platform.Length);
    }

    private void FixedUpdate()
    {
        //����ĳ��Ʈ ���� �߻�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.9f, HitLayer);
        //���⼭ 5�� ����

        if(hit.collider != null)
        {
            ScanObject = hit.collider.gameObject;
            Debug.Log("��������" + hit.collider.gameObject.name);

            Rigidbody2D scanObjectRigidBody2D = ScanObject.GetComponent<Rigidbody2D>();

            if(scanObjectRigidBody2D != null)
            {
                scanObjectRigidBody2D.excludeLayers = LayerMask.GetMask("Player");
            }
        }
        if (hit.collider == null)
        {
            // ����ĳ��Ʈ�� �ƹ��͵� �浹���� �ʾ��� �� ����Ǵ� �ڵ�
            for (int i = 0; i < _platform.Length; i++)
            {
                // �÷��� ������Ʈ���� �ϳ��� �ݺ�
                Rigidbody2D platformRigidBody = _platform[i].GetComponent<Rigidbody2D>();

                if (platformRigidBody != null)
                {
                    // ���� �÷��� ������Ʈ�� Rigidbody2D�� �ִٸ�
                    platformRigidBody.excludeLayers = 0;  // excludeLayers�� 0���� ����
                                                          // '0'�� �����ϴ� ���� �ش� Rigidbody2D�� �浹�� �����ϴ� ���·� ����� ��
                }
            }
        }



        Debug.DrawRay(transform.position, Vector2.up * 0.9f, Color.green);

       // updateCollision();//��ĵ�Ѱ� �浹 ��Ȱ��, ������ ���� �浹

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
