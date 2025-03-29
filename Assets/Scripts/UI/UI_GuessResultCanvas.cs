using TMPro;
using System;
using UnityEngine;

public class UI_GuessResultCanvas : MonoBehaviour
{
    Canvas _guessResultCanvas;

    TextMeshProUGUI _playerPointText;   // 플레이어 점수
    TextMeshProUGUI _enemyPointText;    // 적 점수 
    TextMeshProUGUI _playerGuessText;   // 플레이어가 생각한 적의 up, blackjack, down
    TextMeshProUGUI _enemyGuessText;    // 적이 생각한 플레이어의 up, blackjack, down

    void Awake()
    {
        _guessResultCanvas = GetComponent<Canvas>();
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        _playerPointText = texts[0];
        _enemyPointText = texts[1];
        _playerGuessText = texts[2];
        _enemyGuessText = texts[3];
    }

    void Start()
    {
        UIManager.Instance.toggleGuessResultCanvasAction += ToggleGuessResult;
        UIManager.Instance.disableAction += Disable;
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
            || (GameManager.Instance.PlayerGuess == Define.Guess.Spot && enemyPoint == 21)
            || (GameManager.Instance.PlayerGuess == Define.Guess.Down && enemyPoint < 21))
        {
            _playerGuessText.color = Color.white;
            playerWin = true;
        }
        if ((GameManager.Instance.EnemyGuess == Define.Guess.Up && playerPoint > 21)
            || (GameManager.Instance.EnemyGuess == Define.Guess.Spot && playerPoint == 21)
            || (GameManager.Instance.EnemyGuess == Define.Guess.Down && playerPoint < 21))
        {
            _enemyGuessText.color = Color.white;
            enemyWin = true;
        }

        StartCoroutine(GameManager.Instance.CameraController.GuessResultDirection(3f, playerWin, enemyWin));
    }

    // 플레이어 점수(카드 숫자 총합) 보여주기 => 총 쏘는 연출로 넘어감
    public void ToggleGuessResult()
    {

        UIManager.Instance.DisableAllCanvas();
        Tuple<int, int> points = CardManager.Instance.CalculatePoint();
        _guessResultCanvas.GetComponent<UI_GuessResultCanvas>().SetRoundResult();
        _guessResultCanvas.enabled = !_guessResultCanvas.enabled;
        //디시전 표시
        //points.item1 = 적 점수
        //points.item2 = 플레이어 점수



        Debug.Log(points.Item1 + " , " + points.Item2);
    }

    public void Disable()
    {
        _guessResultCanvas.enabled = false;
    }
}