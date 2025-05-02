using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public static Achievement Instance;

    public GameObject[] LockObjects;
    public GameObject[] Achievements;

    public Animator AchieveAnimator;
    public Image GoBackImage;

    private void Start()
    {
        Instance = this;


        //아직 저장 기능을 안 만든 관계로...
        for(int i = 0; i<Achievements.Length; i++)
        {
            Achievements[i].SetActive(false);
        }

        for (int i = 0; i < LockObjects.Length; i++)
        {
            LockObjects[i].SetActive(true);
        }
    }

    public void GoHome()
    {
        //
        AchieveAnimator.SetTrigger("Move_Right");
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
}
