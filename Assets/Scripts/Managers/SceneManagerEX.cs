using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{

    void Start()
    {
        int winStreak = DataManager.Instance.GameData.winStreak;
        SwitchScene(winStreak);
    }

    void SwitchScene(int winStreak)
    {
        if(winStreak == 5)
        {
            Debug.Log("토너먼트 우승");

            // 토너먼트 우승 횟수 증가
            // 연승 기록 초기화
            winStreak = 0;
            DataManager.Instance.GameData.tournamentWinCount++;
            DataManager.Instance.GameData.winStreak = winStreak;
            DataManager.Instance.Save();
        }

        // 각 우승횟수에 맞는 씬으로 이동
        SceneManager.LoadScene(winStreak + 1);
    }
}
