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
        //검게 되는 애니메이션
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
                HomeManager.Instance.HomePanel[0].SetActive(true);//홈
                HomeManager.Instance.HomePanel[1].SetActive(false);//캐릭터

                TitleBgm.SetActive(false);
                HomeBgm.SetActive(true);
                GameBgm.SetActive(false);

                StartCoroutine(GoGame1());
            }

            IEnumerator GoGame1()
            {
                yield return new WaitForSeconds(0.5f);
                //비어있는 애니메이션
                Fade.Instance.Go_Empty();
                homeManager.enabled = true;
                homeManager.GoHome();
            }
        }

        
    }
}
