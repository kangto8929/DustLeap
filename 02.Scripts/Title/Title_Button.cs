using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title_Button : MonoBehaviour
{
    public static Title_Button Instance;

    public GameObject TitleCanvas;
    public GameObject HomeCanvas;

    public GameObject TitleBgm;
    public GameObject HomeBgm;
    public GameObject GameBgm;

    public HomeManager homeManager;

    public bool StartFade = false;

    private void Start()
    {
        Instance = this;

        

        TitleBgm.SetActive(true);
        HomeBgm.SetActive(false);
        GameBgm.SetActive(false);
    }

    public void StartGame()
    {
        //�˰� �Ǵ� �ִϸ��̼�
        if(StartFade == false)
        {
            StartFade = true;

            Fade.Instance.Go_Black();



            StartCoroutine(GoGame());
            IEnumerator GoGame()
            {
                yield return new WaitForSeconds(1.4f);
                TitleCanvas.SetActive(false);
                HomeCanvas.SetActive(true);
                HomeManager.Instance.HomePanel[0].SetActive(true);//Ȩ
                HomeManager.Instance.HomePanel[1].SetActive(false);//ĳ����

                TitleBgm.SetActive(false);
                HomeBgm.SetActive(true);
                GameBgm.SetActive(false);

                StartCoroutine(GoGame1());
            }

            IEnumerator GoGame1()
            {
                yield return new WaitForSeconds(0.5f);
                //����ִ� �ִϸ��̼�
                Fade.Instance.Go_Empty();
                homeManager.enabled = true;
                homeManager.GoHome();
            }
        }

        
    }
}
