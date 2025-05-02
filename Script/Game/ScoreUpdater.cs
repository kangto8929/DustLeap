using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    public static ScoreUpdater Instance;

    public TextMeshProUGUI ScoreText;
    public Transform player; // �÷��̾��� Transform
    public float minY = 0f; // Y �ּҰ� (0%)
    public float maxY = 60f; // Y �ִ밪 (100%)
    private float lastScore = 0f; // ���������� ����� ����
    private float timeSinceLastScore = 0f; // ������ ���ŵ� �� ��� �ð�
    public float scoreCooldown = 0.5f; // ���� ���� �� ��� �ð� (��)

    private void Start()
    {
        Instance = this;

        // �ʱ� ���� ����
        lastScore = 0f; // �ʱ� ���� ����
        ScoreText.text = lastScore.ToString("F2") + "%";
        Debug.Log("�ʱ� ����: " + lastScore.ToString("F2") + "%");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeSinceLastScore >= scoreCooldown)
        {
            // ���� ���
            float playerY = player.position.y;
            float percentage = 0f;

            if (playerY >= minY && playerY <= maxY)
            {
                percentage = Mathf.Clamp01((playerY - minY) / (maxY - minY)) * 100f;

                if(percentage > 49)
                {
                    //���� ����
                    Achievement.Instance.LockObjects[2].SetActive(false);
                    Achievement.Instance.Achievements[2].SetActive(true);
                }
            }
            else if (playerY < minY)
            {
                percentage = 0f;
            }
            else if (playerY > maxY)
            {
                percentage = 100f;

                EndGame.Instance.PlayerAnimation.SetFloat("Speed", 0f);

                //���� ����
                Achievement.Instance.LockObjects[3].SetActive(false);
                Achievement.Instance.Achievements[3].SetActive(true);

                EndGame.Instance.PlayerControl.enabled = false;
                EndGame.Instance.PlayerRigidBody2d.simulated = false;
                EndGame.Instance.IsRunning = false;

                if (!EndGame.Instance.EndTrigger)
                {
                    EndGame.Instance.EndTrigger = true; // �� ���� ����ǵ��� ����

                    StartCoroutine(Endgame());
                    EndGame.Instance.StopTimer();
                    IEnumerator Endgame()
                    {
                        yield return new WaitForSeconds(0.1f);
                        Debug.Log("���� ��!");
                        //EndGame.Instance.PlayerControl.enabled = false;
                        //EndGame.Instance.IsRunning = false;
                        EndGame.Instance.ResultPanel.SetActive(true);
                       // EndGame.Instance.StopTimer();
                    }

                    
                }
            }

            // �Ҽ��� �� ��° �ڸ����� ����
            percentage = float.Parse(percentage.ToString("F2"));

            //  **�Ҽ��� ù ��° �ڸ� �̻� ��ȭ�� ���� ���� ����**
            if ((int)percentage != (int)lastScore)
            {
                lastScore = percentage; // ���ο� ���� ����
                ScoreText.text = lastScore.ToString("F2") + "%";
                Debug.Log("���� ����: " + lastScore.ToString("F2") + "%");
            }
            else
            {
                Debug.Log("�Ҽ��� �� ��° �ڸ��� ����� �� ���� ����: " + lastScore.ToString("F2") + "%");
            }

            // ������ ���ŵ� �� ��� �ð� ����
            timeSinceLastScore = 0f;
        }
    }

    void Update()
    {
        timeSinceLastScore += Time.deltaTime;
    }
}
