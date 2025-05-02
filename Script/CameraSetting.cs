using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public GameObject[] Cameras;
    public GameObject GameScene;//���� ȭ������, ���� ȭ������ ��


    public void FixedUpdate()
    {
        if(GameScene.activeSelf == true)
        {
            //���� ���� ȭ���� Ȱ��ȭ
            //���� ī�޶� Ȱ��
            Cameras[0].SetActive(true);

            Cameras[1].SetActive(false);

            Title_Button.Instance.HomeBgm.SetActive(false);
            Title_Button.Instance.GameBgm.SetActive(true);
        }

        else
        {
            //������ ī�޶� Ȱ��
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
