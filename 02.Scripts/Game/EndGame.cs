using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class EndGame : MonoBehaviour
{
    public CameraSetting Camerasetting;

    //���� �������� �˷��ִ� UI
    public DustController PlayerControl;
    public GameObject ResultPanel;

    public TextMeshProUGUI TimerText;
    public float Timer = 0f;//Ÿ�̸�
    public bool IsRunning = false;//�ð��� �帣�� �ִ���

    public bool EndTrigger = false; // �� ���� ����ǵ��� ����

    public static EndGame Instance;

    public TextMeshProUGUI StarCandyText;
    public int CandyCount = 0;

    public int MenuTouch = 0;//��ġ ī��Ʈ
    public GameObject Menu;

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

    public void GoMenu()
    {
        MenuTouch++;
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

    public void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(IsRunning)
        {
            //���� �ð��� �帣�� �ִٸ�
            Timer += Time.deltaTime;
            UpdateTimerUI();//Ÿ�̸� ������Ʈ
        }
    }

    void UpdateTimerUI()
    {
        //Ÿ�̸� ������Ʈ
        //00:00:00����
        int hours = Mathf.FloorToInt(Timer / 3600);//�ð�
        int minutes = Mathf.FloorToInt((Timer % 3600)) / 60;//��
        int seconds = Mathf.FloorToInt(Timer % 60);//��

        TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        //ĵ�� ���� ���
        int minCandy = 50;//1�ð� �̻� �ɸ��� �ּ� �߰� ����
        int maxCandy = 500;//��� Ŭ���� �� �ִ� �߰� ����
        float maxTime = 3500f;//1�ð� ����

        int extraCandy;

        if(Timer >= maxTime)
        {
            extraCandy = minCandy;//1�ð� �̻��̸� �ּ� ���� 50��
        }

        else
        {
            float normalizedTime = Timer / maxTime;//0���~1�ð�
            extraCandy = Mathf.RoundToInt(Mathf.Lerp(maxCandy, minCandy, normalizedTime));
        }

        CandyCount = 100 + extraCandy;//�⺻ 100�� + �߰� ����

        StarCandyText.text = "x  " + CandyCount.ToString();
    }


    //Ÿ�̸� ����
    public void StartTimer()
    {
        Timer = 0f;
        IsRunning = true;
        //ResultPanel.SetActive(false);
    }

    //Ÿ�̸� ����
    public void StopTimer()
    {
        PlayerControl.enabled = false;
        IsRunning = false;
        //ResultPanel.SetActive(true);
        UpdateTimerUI();//���� �ð� ������Ʈ
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
            //Ȩ ȭ�� Ȱ��
            HomeManager.Instance.Home.SetActive(true);
            HomeManager.Instance.HomePanel[0].SetActive(true);//Ȩ
            HomeManager.Instance.HomePanel[1].SetActive(false);//ĳ����
            //���� ȭ�� ��Ȱ��
            Camerasetting.GameScene.SetActive(false);

            //MenuTouch = 0;
            if (MenuTouch % 2 == 0)
            {
                //¦��
                Menu.SetActive(false);
            }

            if (MenuTouch % 2 == 1)
            {
                //Ȧ��
                MenuTouch++;
                Menu.SetActive(false);
            }

            //Ȩ��
            if (HomeManager.Instance.MenuTouch % 2 == 0)
            {
                //¦��
                Menu.SetActive(false);
            }

            if(HomeManager.Instance.MenuTouch % 2 == 1)
            {
                //Ȧ��
                HomeManager.Instance.MenuTouch++;
                Menu.SetActive(false);
            }


            StartCoroutine(GoGame1());
        }

        IEnumerator GoGame1()
        {
            yield return new WaitForSeconds(1.4f);//2.5
            //����ִ� �ִϸ��̼�
            Fade.Instance.Go_Empty();
        }
    }

    public void ReStart()
    {
        //�˰� �Ǵ� �ִϸ��̼�
        PlayerControl.enabled = false;
        Fade.Instance.Go_Black();

        StartCoroutine(GoGame());
        IEnumerator GoGame()
        {
            yield return new WaitForSeconds(1.4f);
            ResultPanel.SetActive(false);
            

            //Ȩ ȭ�� ��Ȱ��
           // HomeManager.Instance.Home.SetActive(false);
            //���� ȭ�� Ȱ��
            Camerasetting.GameScene.SetActive(true);
            ScoreUpdater.Instance.player.position = new Vector2(-0.03227263f, -7.1f);
            //MenuTouch = 0;
            if (MenuTouch % 2 == 0)
            {
                //¦��
                Menu.SetActive(false);
            }

            else
            {
                //Ȧ��
                MenuTouch++;
                Menu.SetActive(false);
            }
            StartCoroutine(GoGame1());
        }

        IEnumerator GoGame1()
        {
            yield return new WaitForSeconds(2.5f);
            //����ִ� �ִϸ��̼�
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
