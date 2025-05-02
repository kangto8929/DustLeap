using UnityEngine;
using UnityEngine.UI;


public class Setting_Manager : MonoBehaviour
{
    public static Setting_Manager Instance;

    public Animator SettingAnimator;
    public Image GoBackImage;

    public void Awake()
    {
        Instance = this;
    }

    public void GoHome()
    {
        //
        SettingAnimator.SetTrigger("Move_Right");
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
