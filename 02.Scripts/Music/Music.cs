using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public static Music Instance;
    public Animator MusicAnimator;
    public Image GoBackImage;

    public int CurrentMusic = 0;//현재 몇 번째 음악이 나오는지
    public GameObject[] PlayMusic;//현재 재생 중이라고 뜨는 거
    public GameObject[] DefaultMusic;//기본 상태 음악들
    public GameObject[] Musics;

    public void Start()
    {
        Instance = this;

        SelectMusic(0); // 처음 시작할 때 0번이 선택되도록 설정
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

    //음악 선택 버튼
    public void SelectMusic(int selectedIndex)
    {
        //선택된 번호만 활성화, 나머지는 비활성화
        for(int i = 0; i<PlayMusic.Length; i++)
        {
            PlayMusic[i].SetActive(i == selectedIndex);
        }

        //선택된 번호만 비활성화, 나머지는 활성화
        for(int i = 0; i<DefaultMusic.Length; i++)
        {
            DefaultMusic[i].SetActive(i != selectedIndex);
        }


        //선택된 번호만 활성화, 나머지는 비활성화
        for(int i = 0; i< Musics.Length; i++)
        {
            Musics[i].SetActive(i == selectedIndex);
        }
    }

}
