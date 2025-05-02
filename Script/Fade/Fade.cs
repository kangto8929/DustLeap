using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator FadeAnimation;
    public static Fade Instance;
    private void Start()
    {
        Instance = this;
    }

    public void Go_Black()
    {
        FadeAnimation.SetTrigger("Go_Black");
    }

    public void Go_Empty()
    {
        FadeAnimation.SetTrigger("Go_Empty");
    }
}
