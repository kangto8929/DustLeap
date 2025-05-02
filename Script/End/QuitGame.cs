using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public GameObject End_Panel;//���� ���� ȭ��

    private void Start()
    {
        End_Panel.SetActive(false);
    }

    private void Update()
    {
        //���� ����
        if (Input.GetButtonDown("Cancel"))
        {
            End_Panel.SetActive(true);
        }

    }

    public void End_Game_Yes()
    {
        //���� ���� - ��
        //End_Panel[0].SetActive(false);
        //End_Panel[1].SetActive(false);

        End_Game();
    }

    public void End_Game_No()//
    {
        //���� ���� - �ƴϿ�
        End_Panel.SetActive(false);
    }

    public void End_Game()
    {
#if UNITY_EDITOR//����Ƽ �������� ���
        UnityEditor.EditorApplication.isPlaying = false;
#else//���� ������ ���
        Application.Quit(); // ���ø����̼� ����
#endif
    }

}
