using TMPro;
using System;
using UnityEngine;

public class UI_GuessResultCanvas : MonoBehaviour
{
    TextMeshProUGUI _playerPointText;   // 플레이어 점수
    TextMeshProUGUI _enemyPointText;    // 적 점수 
    TextMeshProUGUI _playerGuessText;   // 플레이어가 생각한 적의 up, blackjack, down
    TextMeshProUGUI _enemyGuessText;    // 적이 생각한 플레이어의 up, blackjack, down

    void Start()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        _playerPointText = texts[0];
        _enemyPointText = texts[1];
        _playerGuessText = texts[2];
        _enemyGuessText = texts[3];
    }

    // UI Update
    public void SetRoundResult()
    {
        // 점수
        Tuple<int, int> point = CardManager.Instance.CalculatePoint();
        int playerPoint = point.Item1, enemyPoint = point.Item2;
        _playerPointText.text = playerPoint.ToString();
        _enemyPointText.text = enemyPoint.ToString();

        // 추측
        _playerGuessText.text = GameManager.Instance.PlayerGuess.ToString();
        _enemyGuessText.text = GameManager.Instance.EnemyGuess.ToString();

        _playerGuessText.color = Color.red;
        _enemyGuessText.color = Color.red;

        bool playerWin = false, enemyWin = false;
        if ((GameManager.Instance.PlayerGuess == Define.Guess.Up && enemyPoint > 21)
            || (GameManager.Instance.PlayerGuess == Define.Guess.BlackJack && enemyPoint == 21)
            || (GameManager.Instance.PlayerGuess == Define.Guess.Down && enemyPoint < 21))
        {
            _playerGuessText.color = Color.white;
            playerWin = true;
        }
        if ((GameManager.Instance.EnemyGuess == Define.Guess.Up && playerPoint > 21)
            || (GameManager.Instance.EnemyGuess == Define.Guess.BlackJack && playerPoint == 21)
            || (GameManager.Instance.EnemyGuess == Define.Guess.Down && playerPoint < 21))
        {
            _enemyGuessText.color = Color.white;
            enemyWin = true;
        }

        StartCoroutine(GameManager.Instance.CameraController.GuessResultDirection(3f, playerWin, enemyWin));
    }
}