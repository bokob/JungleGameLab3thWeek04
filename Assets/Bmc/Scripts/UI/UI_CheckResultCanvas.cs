using TMPro;
using System;
using UnityEngine;

public class UI_CheckResultCanvas : MonoBehaviour
{
    TextMeshProUGUI _playerPointText;
    TextMeshProUGUI _enemyPointText;
    TextMeshProUGUI _playerGuessText;
    TextMeshProUGUI _enemyGuessText;

    void Start()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        _playerPointText = texts[0];
        _enemyPointText = texts[1];
        _playerGuessText = texts[2];
        _enemyGuessText = texts[3];
    }

    public void SetRoundResult()
    {
        // 점수
        Tuple<int, int> point = CardManager.Instance.CalculatePoint();
        int enemyPoint = point.Item1;
        int playerPoint = point.Item2;
        _enemyPointText.text = enemyPoint.ToString();
        _playerPointText.text = playerPoint.ToString();

        // 추측
        _playerGuessText.text = GameManager.Instance.PlayerGuess.ToString();
        _enemyGuessText.text = GameManager.Instance.EnemyGuess.ToString();

        _playerGuessText.color = Color.red;
        _enemyGuessText.color = Color.red;

        bool playerWin = false;
        bool enemyWin = false;
        if ((GameManager.Instance.PlayerGuess == Bmc.Define.Decision.Up && enemyPoint > 21)
            || (GameManager.Instance.PlayerGuess == Bmc.Define.Decision.BlackJack && enemyPoint == 21)
            || (GameManager.Instance.PlayerGuess == Bmc.Define.Decision.Down && enemyPoint < 21))
        {
            _playerGuessText.color = Color.green;
            playerWin = true;
        }
        if ((GameManager.Instance.EnemyGuess == Bmc.Define.Decision.Up && playerPoint > 21)
            || (GameManager.Instance.EnemyGuess == Bmc.Define.Decision.BlackJack && playerPoint == 21)
            || (GameManager.Instance.EnemyGuess == Bmc.Define.Decision.Down && playerPoint < 21))
        {
            _enemyGuessText.color = Color.green;
            enemyWin = true;
        }
        GameManager.Instance.Shoot(playerWin, enemyWin);
    }
}