using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public GameObject End_Panel;//게임 종료 화면

    private void Start()
    {
        End_Panel.SetActive(false);
    }

    private void Update()
    {
        //게임 종료
        if (Input.GetButtonDown("Cancel"))
        {
            End_Panel.SetActive(true);
        }

    }

    public void End_Game_Yes()
    {
        //게임 종료 - 예
        //End_Panel[0].SetActive(false);
        //End_Panel[1].SetActive(false);

        End_Game();
    }

    public void End_Game_No()//
    {
        //게임 종료 - 아니오
        End_Panel.SetActive(false);
    }

    public void End_Game()
    {
#if UNITY_EDITOR//유니티 에디터일 경우
        UnityEditor.EditorApplication.isPlaying = false;
#else//실제 어플일 경우
        Application.Quit(); // 어플리케이션 종료
#endif
    }

}
