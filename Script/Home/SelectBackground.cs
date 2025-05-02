using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectBackground : MonoBehaviour
{

    public static SelectBackground Instance;
    public GameObject[] Backgrounds; // 배경 배열
    public Image[] ButtonImage;//버튼 이미지들

    public int Count = 5;

    private Color _targetColor = new Color(0.6830188f, 0.3492416f, 0.3492416f, 1f); //바뀌는 색상

    private void Start()
    {
        Instance = this;
        Count = 5;
    }

    public void LandScape()
    {
        ButtonImage[0].color = _targetColor;

        for(int i = 1; i<4; i++)
        {
            ButtonImage[i].color = new Color(0f, 0f, 0f, 1f);
        }

        //배경 변경
        Backgrounds[0].SetActive(true);

        for (int i = 1; i < 4; i++)
        {
            Backgrounds[i].SetActive(false);
        }
    }

    public void Summer()
    {
        ButtonImage[1].color = _targetColor;

        ButtonImage[0].color = new Color(0f, 0f, 0f, 1f);
        ButtonImage[2].color = new Color(0f, 0f, 0f, 1f);
        ButtonImage[3].color = new Color(0f, 0f, 0f, 1f);

        //배경 변경
        Backgrounds[1].SetActive(true);

        Backgrounds[0].SetActive(false);
        Backgrounds[2].SetActive(false);
        Backgrounds[3].SetActive(false);
    }

    public void Fall()
    {
        ButtonImage[2].color = _targetColor;

        ButtonImage[0].color = new Color(0f, 0f, 0f, 1f);
        ButtonImage[1].color = new Color(0f, 0f, 0f, 1f);
        ButtonImage[3].color = new Color(0f, 0f, 0f, 1f);

        //배경 변경
        //배경 변경
        Backgrounds[2].SetActive(true);

        Backgrounds[0].SetActive(false);
        Backgrounds[1].SetActive(false);
        Backgrounds[3].SetActive(false);
    }

    public void Winter()
    {
        ButtonImage[3].color = _targetColor;

        ButtonImage[0].color = new Color(0f, 0f, 0f, 1f);
        ButtonImage[1].color = new Color(0f, 0f, 0f, 1f);
        ButtonImage[2].color = new Color(0f, 0f, 0f, 1f);

        //배경 변경
        Backgrounds[3].SetActive(true);

        Backgrounds[0].SetActive(false);
        Backgrounds[1].SetActive(false);
        Backgrounds[2].SetActive(false);
    }

    public void BackGroundSelect()
    {
        if (ButtonImage[0].color == _targetColor)
        {
            Backgrounds[0].SetActive(true);
            Backgrounds[1].SetActive(false);
            Backgrounds[2].SetActive(false);
            Backgrounds[3].SetActive(false);

            Count = 0;

            Achievement.Instance.Achievements[4].SetActive(true);
            Achievement.Instance.LockObjects[4].SetActive(false);
        }

        else if (ButtonImage[1].color == _targetColor)
        {
            Backgrounds[0].SetActive(false);
            Backgrounds[1].SetActive(true);
            Backgrounds[2].SetActive(false);
            Backgrounds[3].SetActive(false);

            Count = 1;

            Achievement.Instance.Achievements[4].SetActive(true);
            Achievement.Instance.LockObjects[4].SetActive(false);
        }

        else if (ButtonImage[2].color == _targetColor)
        {
            Backgrounds[0].SetActive(false);
            Backgrounds[1].SetActive(false);
            Backgrounds[2].SetActive(true);
            Backgrounds[3].SetActive(false);

            Count = 2;

            Achievement.Instance.Achievements[4].SetActive(true);
            Achievement.Instance.LockObjects[4].SetActive(false);
        }

        else if (ButtonImage[3].color == _targetColor)
        {
            Backgrounds[0].SetActive(false);
            Backgrounds[1].SetActive(false);
            Backgrounds[2].SetActive(false);
            Backgrounds[3].SetActive(true);

            Count = 3;

            Achievement.Instance.Achievements[4].SetActive(true);
            Achievement.Instance.LockObjects[4].SetActive(false);
        }

        else
        {
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            ButtonImage[i].color = new Color(0f, 0f, 0f, 1f);
        }

        
    }




}
