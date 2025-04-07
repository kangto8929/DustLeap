using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class EndGame : MonoBehaviour
{
    public CameraSetting Camerasetting;

    //게임 끝났음을 알려주는 UI
    public DustController PlayerControl;
    public GameObject ResultPanel;

    public TextMeshProUGUI TimerText;
    public float Timer = 0f;//타이머
    public bool IsRunning = false;//시간이 흐르고 있는지

    public bool EndTrigger = false; // 한 번만 실행되도록 설정

    public static EndGame Instance;

    public TextMeshProUGUI StarCandyText;
    public int CandyCount = 0;

    public int MenuTouch = 0;//터치 카운트
    public GameObject Menu;

    private void FixedUpdate()
    {
        if (MenuTouch % 2 == 0)
        {
            //짝수
            Menu.SetActive(false);
        }

        else
        {
            //홀수
            Menu.SetActive(true);
        }
    }

    public void GoMenu()
    {
        MenuTouch++;
        if (MenuTouch % 2 == 0)
        {
            //짝수
            Menu.SetActive(false);
        }

        else
        {
            //홀수
            Menu.SetActive(true);
        }
    }

    public void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(IsRunning)
        {
            //현재 시간이 흐르고 있다면
            Timer += Time.deltaTime;
            UpdateTimerUI();//타이머 업데이트
        }
    }

    void UpdateTimerUI()
    {
        //타이머 업데이트
        //00:00:00형식
        int hours = Mathf.FloorToInt(Timer / 3600);//시간
        int minutes = Mathf.FloorToInt((Timer % 3600)) / 60;//분
        int seconds = Mathf.FloorToInt(Timer % 60);//초

        TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        //캔디 갯수 계산
        int minCandy = 50;//1시간 이상 걸리면 최소 추가 보상
        int maxCandy = 500;//즉시 클리어 시 최대 추가 보상
        float maxTime = 3500f;//1시간 기준

        int extraCandy;

        if(Timer >= maxTime)
        {
            extraCandy = minCandy;//1시간 이상이면 최소 보상 50개
        }

        else
        {
            float normalizedTime = Timer / maxTime;//0즉시~1시간
            extraCandy = Mathf.RoundToInt(Mathf.Lerp(maxCandy, minCandy, normalizedTime));
        }

        CandyCount = 100 + extraCandy;//기본 100개 + 추가 보상

        StarCandyText.text = "x  " + CandyCount.ToString();
    }


    //타이머 시작
    public void StartTimer()
    {
        Timer = 0f;
        IsRunning = true;
        //ResultPanel.SetActive(false);
    }

    //타이머 정지
    public void StopTimer()
    {
        PlayerControl.enabled = false;
        IsRunning = false;
        //ResultPanel.SetActive(true);
        UpdateTimerUI();//최종 시간 업데이트
    }

    

    public void GoHome()
    {
        ResultPanel.SetActive(false);
        PlayerControl.enabled = false;

        Fade.Instance.Go_Black();

        StartCoroutine(GoGame());
        IEnumerator GoGame()
        {
            yield return new WaitForSeconds(1.4f);
            ScoreUpdater.Instance.player.position = new Vector2(-0.03227263f, -7.1f);
            ResultPanel.SetActive(false);
            //홈 화면 활성
            HomeManager.Instance.Home.SetActive(true);
            HomeManager.Instance.HomePanel[0].SetActive(true);//홈
            HomeManager.Instance.HomePanel[1].SetActive(false);//캐릭터
            //게임 화면 비활성
            Camerasetting.GameScene.SetActive(false);

            //MenuTouch = 0;
            if (MenuTouch % 2 == 0)
            {
                //짝수
                Menu.SetActive(false);
            }

            if (MenuTouch % 2 == 1)
            {
                //홀수
                MenuTouch++;
                Menu.SetActive(false);
            }

            //홈거
            if (HomeManager.Instance.MenuTouch % 2 == 0)
            {
                //짝수
                Menu.SetActive(false);
            }

            if(HomeManager.Instance.MenuTouch % 2 == 1)
            {
                //홀수
                HomeManager.Instance.MenuTouch++;
                Menu.SetActive(false);
            }


            StartCoroutine(GoGame1());
        }

        IEnumerator GoGame1()
        {
            yield return new WaitForSeconds(1.4f);//2.5
            //비어있는 애니메이션
            Fade.Instance.Go_Empty();
        }
    }

    public void ReStart()
    {
        //검게 되는 애니메이션
        PlayerControl.enabled = false;
        Fade.Instance.Go_Black();

        StartCoroutine(GoGame());
        IEnumerator GoGame()
        {
            yield return new WaitForSeconds(1.4f);
            ResultPanel.SetActive(false);
            

            //홈 화면 비활성
           // HomeManager.Instance.Home.SetActive(false);
            //게임 화면 활성
            Camerasetting.GameScene.SetActive(true);
            ScoreUpdater.Instance.player.position = new Vector2(-0.03227263f, -7.1f);
            //MenuTouch = 0;
            if (MenuTouch % 2 == 0)
            {
                //짝수
                Menu.SetActive(false);
            }

            else
            {
                //홀수
                MenuTouch++;
                Menu.SetActive(false);
            }
            StartCoroutine(GoGame1());
        }

        IEnumerator GoGame1()
        {
            yield return new WaitForSeconds(2.5f);
            //비어있는 애니메이션
            Fade.Instance.Go_Empty();
            PlayerControl.enabled = true;

            StartCoroutine(GoGame2());
        }

        IEnumerator GoGame2()
        {
            yield return new WaitForSeconds(0.5f);
            IsRunning = true;
            EndTrigger = false;
            StartTimer();
        }

    }
}
