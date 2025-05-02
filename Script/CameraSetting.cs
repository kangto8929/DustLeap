using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public GameObject[] Cameras;
    public GameObject GameScene;//메인 화면인지, 게임 화면인지 등


    public void FixedUpdate()
    {
        if(GameScene.activeSelf == true)
        {
            //만약 게임 화면이 활성화
            //게임 카메라만 활성
            Cameras[0].SetActive(true);

            Cameras[1].SetActive(false);

            Title_Button.Instance.HomeBgm.SetActive(false);
            Title_Button.Instance.GameBgm.SetActive(true);
        }

        else
        {
            //나머지 카메라 활성
            Cameras[0].SetActive(false);

            Cameras[1].SetActive(true);

            
            Title_Button.Instance.GameBgm.SetActive(false);

            if(Title_Button.Instance.TitleCanvas.activeSelf == true)
            {
                Title_Button.Instance.HomeBgm.SetActive(false);
            }
            else
            {
                Title_Button.Instance.HomeBgm.SetActive(true);
            }
        }
    }
}
