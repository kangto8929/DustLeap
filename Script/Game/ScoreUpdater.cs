using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    public static ScoreUpdater Instance;

    public TextMeshProUGUI ScoreText;
    public Transform player; // 플레이어의 Transform
    public float minY = 0f; // Y 최소값 (0%)
    public float maxY = 60f; // Y 최대값 (100%)
    private float lastScore = 0f; // 마지막으로 저장된 점수
    private float timeSinceLastScore = 0f; // 점수가 갱신된 후 경과 시간
    public float scoreCooldown = 0.5f; // 점수 갱신 후 대기 시간 (초)

    private void Start()
    {
        Instance = this;

        // 초기 점수 설정
        lastScore = 0f; // 초기 점수 저장
        ScoreText.text = lastScore.ToString("F2") + "%";
        Debug.Log("초기 점수: " + lastScore.ToString("F2") + "%");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeSinceLastScore >= scoreCooldown)
        {
            // 점수 계산
            float playerY = player.position.y;
            float percentage = 0f;

            if (playerY >= minY && playerY <= maxY)
            {
                percentage = Mathf.Clamp01((playerY - minY) / (maxY - minY)) * 100f;

                if(percentage > 49)
                {
                    //업적 오픈
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

                //업적 오픈
                Achievement.Instance.LockObjects[3].SetActive(false);
                Achievement.Instance.Achievements[3].SetActive(true);

                EndGame.Instance.PlayerControl.enabled = false;
                EndGame.Instance.PlayerRigidBody2d.simulated = false;
                EndGame.Instance.IsRunning = false;

                if (!EndGame.Instance.EndTrigger)
                {
                    EndGame.Instance.EndTrigger = true; // 한 번만 실행되도록 설정

                    StartCoroutine(Endgame());
                    EndGame.Instance.StopTimer();
                    IEnumerator Endgame()
                    {
                        yield return new WaitForSeconds(0.1f);
                        Debug.Log("게임 끝!");
                        //EndGame.Instance.PlayerControl.enabled = false;
                        //EndGame.Instance.IsRunning = false;
                        EndGame.Instance.ResultPanel.SetActive(true);
                       // EndGame.Instance.StopTimer();
                    }

                    
                }
            }

            // 소수점 두 번째 자리까지 유지
            percentage = float.Parse(percentage.ToString("F2"));

            //  **소수점 첫 번째 자리 이상 변화가 있을 때만 갱신**
            if ((int)percentage != (int)lastScore)
            {
                lastScore = percentage; // 새로운 점수 저장
                ScoreText.text = lastScore.ToString("F2") + "%";
                Debug.Log("점수 갱신: " + lastScore.ToString("F2") + "%");
            }
            else
            {
                Debug.Log("소수점 두 번째 자리만 변경됨 → 점수 유지: " + lastScore.ToString("F2") + "%");
            }

            // 점수가 갱신된 후 대기 시간 설정
            timeSinceLastScore = 0f;
        }
    }

    void Update()
    {
        timeSinceLastScore += Time.deltaTime;
    }
}
