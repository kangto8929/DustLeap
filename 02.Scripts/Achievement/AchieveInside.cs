using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchieveInside : MonoBehaviour
{
    public Animator InsideContents;//안에 내용들
    public GameObject ShowInside;//내용물 보여줄 거.

    public void GoInside()
    {
        //내용물 보기
        ShowInside.SetActive(true);
        InsideContents.SetTrigger("Show");
    }

    public void OutInside()
    {
        //내용물 보는 거에서 나가기
        InsideContents.SetTrigger("Empty");

        StartCoroutine(Out());
        IEnumerator Out()
        {
            yield return new WaitForSeconds(0.3f);
            ShowInside.SetActive(false);
        }
    }
}
