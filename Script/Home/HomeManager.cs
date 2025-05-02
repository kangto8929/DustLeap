using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class HomeManager : MonoBehaviour
{
    public GameObject[] Backgrounds; // ��� �迭

    public GameObject Home;//Ȩ ȭ��
    public TextMeshProUGUI StartText;

    public static HomeManager Instance;

    public Animator SettingAnimator;
    public Button[] Homebuttons;

    public int MenuTouch = 0;//��ġ ī��Ʈ
    public GameObject Menu;

    public GameObject[] HomePanel;

    private void FixedUpdate()
    {
        if (MenuTouch % 2 == 0)
        {
            //¦��
            Menu.SetActive(false);
        }

        else
        {
            //Ȧ��
            Menu.SetActive(true);
        }
    }

    //���� ���� ��ư
    public void StartGame()
    {
        //EndGame.Instance.ReStart();
        //Debug.Log("���� ����!");

        EndGame.Instance.PlayerControl.enabled = false;
        Fade.Instance.Go_Black();

        EndGame.Instance.MenuTouch = 0;

        StartCoroutine(GoGame());
        IEnumerator GoGame()
        {
            yield return new WaitForSeconds(1.4f);
            Home.SetActive(false);
            //���� ȭ�� Ȱ��
            EndGame.Instance.Camerasetting.GameScene.SetActive(true);
            EndGame.Instance.PlayerControl.enabled = true;
          //  ScoreUpdater.Instance.player.position = new Vector2(-0.03227263f, -7.1f);

            StartCoroutine(GoGame1());
        }

        IEnumerator GoGame1()
        {
            yield return new WaitForSeconds(1.4f);
            //����ִ� �ִϸ��̼�
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

    //��Ÿ
    public void Awake()
    {
        Instance = this;
        Menu.SetActive(false);
        GoHome();
    }

    public void GoSetting()
    {
        //����
        SettingAnimator.SetTrigger("Move_Left");
    }

    public void GoHome()
    {
        //������
        //�𷡽ð� �׸�
        ColorBlock cb = Homebuttons[0].colors;
        cb.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);
        Homebuttons[0].colors = cb;

        ColorBlock cb1 = Homebuttons[1].colors;
        cb1.normalColor = new Color(1, 1, 1, 1f);
        Homebuttons[1].colors = cb1;

        ColorBlock cb2 = Homebuttons[2].colors;
        cb2.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);
        Homebuttons[2].colors = cb2;

        HomePanel[0].SetActive(true);//Ȩ
        HomePanel[1].SetActive(false);//ĳ����

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
        //���� ����
        Achievement.Instance.AchieveAnimator.SetTrigger("Move_Left");
    }

    public void GoBag()
    {
        //�������ִ� ��
        //�ָӴ� �׸�
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
        //ĳ���� ����
        //ȣ����
        //��� �׸�
        ColorBlock cb2 = Homebuttons[2].colors;
        cb2.normalColor = new Color(1, 1, 1, 1f);
        Homebuttons[2].colors = cb2;

        for (int i = 0; i < 2; i++) // �迭 �ݺ�
        {
            ColorBlock cb = Homebuttons[i].colors; // ���� ���� ���� ��������
            cb.normalColor = new Color(0.53f, 0.53f, 0.53f, 1f);         // Normal Color ����
            Homebuttons[i].colors = cb;            // ����� ColorBlock ����
        }

        HomePanel[0].SetActive(false);//Ȩ
        HomePanel[1].SetActive(true);//ĳ����

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
        //���� ������ ���� ��
        Music.Instance.MusicAnimator.SetTrigger("Move_Left");
    }

    public void GoMenu()
    {
        MenuTouch++;
        if(MenuTouch % 2 == 0)
        {
            //¦��
            Menu.SetActive(false);
        }

        else
        {
            //Ȧ��
            Menu.SetActive(true);
        }
    }
}
