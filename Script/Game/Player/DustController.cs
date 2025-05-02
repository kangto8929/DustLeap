using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
//9.6보다 작을 때 0%


public class DustController : MonoBehaviour
{
    //점수 업데이트
    /*public TextMeshProUGUI ScoreText;
    public Transform player; // 플레이어의 Transform
    public float minY = 0f; // Y 최소값 (0%)
    public float maxY = 60f; // Y 최대값 (100%)
    */

    //이거 기존거
    public float MaxSpeed = 6f;
    public float JumpForce = 1000f;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;

    [HideInInspector]
    public bool LookingRight = true;

    private Rigidbody2D _rigidbody2d;
    private Animator anim;
    private bool _isGrounded = false;

    // 이동 버튼 상태 변수
    private bool _isMovingRight = false;
    private bool _isMovingLeft = false;


    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // 바닥 체크
       


        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15F, WhatIsGround);
        anim.SetBool("IsGrounded", _isGrounded);

        // 점프 (키보드 & UI 버튼 동시 지원)
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
        {
            

            SFX_Manager.Instance.Jump();
            //Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation);

            _rigidbody2d.linearVelocity = new Vector2(_rigidbody2d.linearVelocity.x, 0f);
            _rigidbody2d.AddForce(new Vector2(0, JumpForce));
           /* Black_FootHold.Instance.SetPlatformActive();//부딪히지 않음
            Debug.Log("부딪히면 안돼");

            StartCoroutine(OnCollision());
            IEnumerator OnCollision()
            {
                yield return new WaitForSeconds(0.5f);
                Black_FootHold.Instance.SetPlatformInactive();//부딪힘
            }*/
        }

    }

    void FixedUpdate()
    {


        float horizontal = 0;

        //키보드 입력을 직접 감지해서 
        if (Input.GetKey(KeyCode.LeftArrow) || _isMovingLeft)horizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow) || _isMovingRight) horizontal = 1f;

       /* if ((Input.GetKey(KeyCode.LeftArrow) || _isMovingLeft)
            && (!Input.GetButtonDown("Jump") || !Input.GetKeyDown(KeyCode.Space)))
        {
           // horizontal = -1f;
            Black_FootHold.Instance.SetPlatformInactive();//부딪힘
        }

        if ((Input.GetKey(KeyCode.RightArrow) || _isMovingRight)
            && (!Input.GetButtonDown("Jump") || !Input.GetKeyDown(KeyCode.Space)))
        {
           // horizontal = 1f;
            Black_FootHold.Instance.SetPlatformInactive();//부딪힘
        }*/


            anim.SetFloat("Speed", Mathf.Abs(horizontal));

        _rigidbody2d.linearVelocity = new Vector2(horizontal * MaxSpeed, _rigidbody2d.linearVelocity.y);

        if (horizontal != 0 && ((horizontal > 0 && !LookingRight) || (horizontal < 0 && LookingRight)))
            Flip();

        anim.SetFloat("vSpeed", _rigidbody2d.linearVelocity.y);

        /*float hor = Input.GetAxis("Horizontal"); // 키보드 이동 추가

        // 버튼 입력이 있으면 덮어쓰기
        if (_isMovingRight) hor = 1f;
        if (_isMovingLeft) hor = -1f;

        anim.SetFloat("Speed", Mathf.Abs(hor));
        _rigidbody2d.linearVelocity = new Vector2(hor * MaxSpeed, _rigidbody2d.linearVelocity.y);  // linearVelocity -> velocity로 변경 (최신 Unity에서 권장됨)

        if (hor != 0 && ((hor > 0 && !LookingRight) || (hor < 0 && LookingRight)))
            Flip();

        anim.SetFloat("vSpeed", _rigidbody2d.linearVelocity.y);

        // 사운드 재생 조건을 더 빠르게 체크
        if (hor != 0 && Time.time - _lastSoundTime >= _soundInterval)
        {
            //SFX_Manager.Instance.Jump();
            _lastSoundTime = Time.time;  // 효과음 재생 후 시간 업데이트
        }*/

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // 좌우 이동 버튼을 눌렀을 때는 이펙트가 나오지 않도록 체크
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump and Fall") && collision2D.relativeVelocity.magnitude > 4)
        {
            Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation);
        }
    }


    public void Flip()
    {
        LookingRight = !LookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    // 점프 버튼 (UI 버튼용)
    public void MiddleButton()
    {
        if (_isGrounded)
        {
            SFX_Manager.Instance.Jump();
            _rigidbody2d.linearVelocity = new Vector2(_rigidbody2d.linearVelocity.x, 0f);
            _rigidbody2d.AddForce(new Vector2(0, JumpForce));

        }
    }

    // 오른쪽 이동 시작
    public void MoveRightStart()
    {
        //SFX_Manager.Instance.Jump();
        _isMovingRight = true;
    }

    // 왼쪽 이동 시작
    public void MoveLeftStart()
    {
        //SFX_Manager.Instance.Jump();
        _isMovingLeft = true;
    }

    // 오른쪽 이동 정지
    public void MoveRightStop()
    {
        _isMovingRight = false;
    }

    // 왼쪽 이동 정지
    public void MoveLeftStop()
    {
        _isMovingLeft = false;
    }

    
}