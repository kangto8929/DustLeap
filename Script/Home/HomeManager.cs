using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class HomeManager : MonoBehaviour
{
    public GameObject[] Backgrounds; // 배경 배열

    public GameObject Home;//홈 화면
    public TextMeshProUGUI StartText;

    public static HomeManager Instance;

    public Animator SettingAnimator;
    public Button[] Homebuttons;

    public int MenuTouch = 0;//터치 카운트
    public GameObject Menu;

    public GameObject[] HomePanel;

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

    //게임 시작 버튼
    public void StartGame()
    {
        //EndGame.Instance.ReStart();
        //Debug.Log("게임 시작!");

        EndGame.Instance.PlayerControl.enabled = false;
        Fade.Instance.Go_Black();

        EndGame.Instance.MenuTouch = 0;

        StartCoroutine(GoGame());
        IEnumerator GoGame()
        {
            yield return new WaitForSeconds(1.4f);
            Home.SetActive(false);
            //게임 화면 활성
            EndGame.Instance.Camerasetting.GameScene.SetActive(true);
            EndGame.Instance.PlayerControl.enabled = true;
          //  ScoreUpdater.Instance.player.position = new Vector2(-0.03227263f, -7.1f);

            StartCoroutine(GoGame1());
        }

        IEnumerator GoGame1()
        {
            yield return new WaitForSeconds(1.4f);
            //비어있는 애니메이션
            ScoreUpdater.Instance.player.position = new Vector2(-0.03227263f, -7.1f);
            Fade.Instance.Go_Empty();
           
            StartCoroutine(GoGame2());
        }

        IEnumerator GoGame2()
        {
            yield return new WaitForSeconds(0.5f);
            EndGame.Instance.IsRunning = true;
            EndGame.Instance.EndTrigger = false;
            EndGame.Instance.StartTimer();
            EndGame.Instance.PlayerRigidBody2d.simulated = true;
        }
    }

    public void PointerEnter()
    {
        StartText.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }

    public void PointerDown()
    {
        StartText.color = new Color(0.35f, 0.35f, 0.35f, 1f);
    }

    public void PointerUp()
    {
        StartText.color = new Color(1f, 1f, 1f, 1f);
    }

    public void PointerExit()
    {
        StartText.color = new Color(1f, 1f, 1f, 1f);
    }

    //기타
    public void Awake()
    {
        Instance = this;
        Menu.SetActive(false);
        GoHome();
    }

    public void GoSetting()
    {
        //설정
        SettingAnimator.SetTrigger("Move_Left");
    }

    public void GoHome()
    {
        //집으로
        //모래시계 그림
        ColorBlock cb = Homebuttons[0].colors;
        cb.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);
        Homebuttons[0].colors = cb;

        ColorBlock cb1 = Homebuttons[1].colors;
        cb1.normalColor = new Color(1, 1, 1, 1f);
        Homebuttons[1].colors = cb1;

        ColorBlock cb2 = Homebuttons[2].colors;
        cb2.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);
        Homebuttons[2].colors = cb2;

        HomePanel[0].SetActive(true);//홈
        HomePanel[1].SetActive(false);//캐릭터

        DustHouch.Instance.TouchImage.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            SelectBackground.Instance.ButtonImage[i].color = new Color(0f, 0f, 0f, 1f);
        }

        if (SelectBackground.Instance.Backgrounds[0].activeSelf == true 
            && SelectBackground.Instance.Count == 0)
        {
            Backgrounds[0].SetActive(true);
            Backgrounds[1].SetActive(false);
            Backgrounds[2].SetActive(false);
            Backgrounds[3].SetActive(false);
        }

        else if (SelectBackground.Instance.Backgrounds[1].activeSelf == true
            && SelectBackground.Instance.Count == 1)
        {
            Backgrounds[0].SetActive(false);
            Backgrounds[1].SetActive(true);
            Backgrounds[2].SetActive(false);
            Backgrounds[3].SetActive(false);
        }

        else if (SelectBackground.Instance.Backgrounds[2].activeSelf == true
            && SelectBackground.Instance.Count == 2)
        {
            Backgrounds[0].SetActive(false);
            Backgrounds[1].SetActive(false);
            Backgrounds[2].SetActive(true);
            Backgrounds[3].SetActive(false);
        }

        else if (SelectBackground.Instance.Backgrounds[3].activeSelf == true
            && SelectBackground.Instance.Count == 3)
        {
            Backgrounds[0].SetActive(false);
            Backgrounds[1].SetActive(false);
            Backgrounds[2].SetActive(false);
            Backgrounds[3].SetActive(true);
        }
    }

    public void GoAchievement()
    {
        //업적 모음
        Achievement.Instance.AchieveAnimator.SetTrigger("Move_Left");
    }

    public void GoBag()
    {
        //아이템있는 곳
        //주머니 그림
        /* ColorBlock cb0 = Homebuttons[0].colors;
         cb0.normalColor = new Color(1, 1, 1, 1f);
         Homebuttons[0].colors = cb0;

         for (int i = 1; i<3; i++)
         {
             ColorBlock cb = Homebuttons[i].colors;
             cb.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);
             Homebuttons[i].colors = cb;
         }*/

        NoEnter.Instance.Notice.SetActive(true);
    }

    public void GoCharacter()
    {
        //캐릭터 관련
        //호감도
        //쿠션 그림
        ColorBlock cb2 = Homebuttons[2].colors;
        cb2.normalColor = new Color(1, 1, 1, 1f);
        Homebuttons[2].colors = cb2;

        for (int i = 0; i < 2; i++) // 배열 반복
        {
            ColorBlock cb = Homebuttons[i].colors; // 현재 색상 정보 가져오기
            cb.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);         // Normal Color 변경
            Homebuttons[i].colors = cb;            // 변경된 ColorBlock 적용
        }

        HomePanel[0].SetActive(false);//홈
        HomePanel[1].SetActive(true);//캐릭터

        DustHouch.Instance.TouchImage.SetActive(true);



        if (Backgrounds[0].activeSelf == true)
        {
            SelectBackground.Instance.Backgrounds[0].SetActive(true);
            SelectBackground.Instance.Backgrounds[1].SetActive(false);
            SelectBackground.Instance.Backgrounds[2].SetActive(false);
            SelectBackground.Instance.Backgrounds[3].SetActive(false);
        }

        else if (Backgrounds[1].activeSelf == true)
        {
            SelectBackground.Instance.Backgrounds[0].SetActive(false);
            SelectBackground.Instance.Backgrounds[1].SetActive(true);
            SelectBackground.Instance.Backgrounds[2].SetActive(false);
            SelectBackground.Instance.Backgrounds[3].SetActive(false);
        }

        else if (Backgrounds[2].activeSelf == true)
        {
            SelectBackground.Instance.Backgrounds[0].SetActive(false);
            SelectBackground.Instance.Backgrounds[1].SetActive(false);
            SelectBackground.Instance.Backgrounds[2].SetActive(true);
            SelectBackground.Instance.Backgrounds[3].SetActive(false);
        }

        else if (Backgrounds[3].activeSelf == true)
        {
            SelectBackground.Instance.Backgrounds[0].SetActive(false);
            SelectBackground.Instance.Backgrounds[1].SetActive(false);
            SelectBackground.Instance.Backgrounds[2].SetActive(false);
            SelectBackground.Instance.Backgrounds[3].SetActive(true);
        }


    }

    public void GoMusicRoom()
    {
        //음악 들으러 가는 곳
        Music.Instance.MusicAnimator.SetTrigger("Move_Left");
    }

    public void GoMenu()
    {
        MenuTouch++;
        if(MenuTouch % 2 == 0)
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
}
