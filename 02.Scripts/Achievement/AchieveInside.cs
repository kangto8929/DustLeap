using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchieveInside : MonoBehaviour
{
    public Animator InsideContents;//�ȿ� �����
    public GameObject ShowInside;//���빰 ������ ��.

    public void GoInside()
    {
        //���빰 ����
        ShowInside.SetActive(true);
        InsideContents.SetTrigger("Show");
    }

    public void OutInside()
    {
        //���빰 ���� �ſ��� ������
        InsideContents.SetTrigger("Empty");

        StartCoroutine(Out());
        IEnumerator Out()
        {
            yield return new WaitForSeconds(0.3f);
            ShowInside.SetActive(false);
        }
    }
}
