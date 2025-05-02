using UnityEngine;

public class NoEnter : MonoBehaviour
{
    public static NoEnter Instance;

    public GameObject Notice;

    private void Start()
    {
        Instance = this;
        Notice.SetActive(false);
    }

    public void Ok()
    {
        Notice.SetActive(false);
    }
}
