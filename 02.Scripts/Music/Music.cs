using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public static Music Instance;
    public Animator MusicAnimator;
    public Image GoBackImage;

    public int CurrentMusic = 0;//���� �� ��° ������ ��������
    public GameObject[] PlayMusic;//���� ��� ���̶�� �ߴ� ��
    public GameObject[] DefaultMusic;//�⺻ ���� ���ǵ�
    public GameObject[] Musics;

    public void Start()
    {
        Instance = this;

        SelectMusic(0); // ó�� ������ �� 0���� ���õǵ��� ����
    }

    public void GoHome()
    {
        MusicAnimator.SetTrigger("Move_Right");
    }

    public void PointerEnter()
    {
        GoBackImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }

    public void PointerDown()
    {
        GoBackImage.color = new Color(0.35f, 0.35f, 0.35f, 1f);
    }

    public void PointerUp()
    {
        GoBackImage.color = new Color(1f, 1f, 1f, 1f);
    }

    public void PointerExit()
    {
        GoBackImage.color = new Color(1f, 1f, 1f, 1f);
    }

    //���� ���� ��ư
    public void SelectMusic(int selectedIndex)
    {
        //���õ� ��ȣ�� Ȱ��ȭ, �������� ��Ȱ��ȭ
        for(int i = 0; i<PlayMusic.Length; i++)
        {
            PlayMusic[i].SetActive(i == selectedIndex);
        }

        //���õ� ��ȣ�� ��Ȱ��ȭ, �������� Ȱ��ȭ
        for(int i = 0; i<DefaultMusic.Length; i++)
        {
            DefaultMusic[i].SetActive(i != selectedIndex);
        }


        //���õ� ��ȣ�� Ȱ��ȭ, �������� ��Ȱ��ȭ
        for(int i = 0; i< Musics.Length; i++)
        {
            Musics[i].SetActive(i == selectedIndex);
        }
    }

}
