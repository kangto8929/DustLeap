using UnityEngine;

public class GameNotice : MonoBehaviour
{
    public static GameNotice Instance;

    public GameObject[] Notice;

    public void Start()
    {
        Instance = this;
    }

    public void ShowReStart()
    {
        Notice[0].SetActive(true);
    }

    public void ShowHome()
    {
        Notice[1].SetActive(true);
    }

    public void No()
    {
        for(int i = 0; i<Notice.Length; i++)
        {
            Notice[i].SetActive(false);
        }
    }

    public void ReStartYes()
    {
        EndGame.Instance.ReStart();
        for (int i = 0; i < Notice.Length; i++)
        {
            Notice[i].SetActive(false);
        }
    }

    public void GoHomeYes()
    {
        EndGame.Instance.GoHome();
        for (int i = 0; i < Notice.Length; i++)
        {
            Notice[i].SetActive(false);
        }
    }
}
