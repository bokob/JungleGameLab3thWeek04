using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    public Define.PlayState CurrentState { get => _currentState; set => _currentState = value; }
    public Revolver Revolver => _revolver;

    Revolver _revolver;
    Define.PlayState _currentState;

    void Start()
    {
        _revolver = GetComponentInChildren<Revolver>();
    }

    // 적이 up, blackJack, down을 고르는 함수 (일단 에이스는 1점으로 계산)
    public void Guess()
    {
        // 현재 덱 정보와 플레이어의 현재 덱 정보 가져옴
        List<Card> deck = CardManager.Instance.Deck;
        List<Card> playerDeck = CardManager.Instance.PlayerDeck;

        // 두 정보에서 평균값 산정
        int cnt = deck.Count + playerDeck.Count;
        int point = 0;
        foreach (Card card in deck)
        {
            point += card.Number;
        }
        foreach (Card card in playerDeck)
        {
            point += card.Number;
        }

        // 플레이어의 패를 평균값으로 가정하고 21보다 큰 지, 작은 지에 따라서 판단
        // up, spot, down
        int avg = point / cnt; // 버림
        if (avg > CardManager.Instance.BlackJack) // up
        {
            GameManager.Instance.EnemyGuess = Define.Guess.Up;
        }
        else if (avg < CardManager.Instance.BlackJack) // down
        {
            GameManager.Instance.EnemyGuess = Define.Guess.Down;
        }
        else // spot
        {
            GameManager.Instance.EnemyGuess = Define.Guess.BlackJack;
        }
    }

    // 카드 뽑기
    public IEnumerator DrawCoroutine()
    {
        int playerPoint = CardManager.Instance.CalculatePoint().Item1;
        int enemyPoint = CardManager.Instance.CalculatePoint().Item2;

        // 일단 뽑고 판단
        do
        {
            CardManager.Instance.DrawCard();
            yield return new WaitForSeconds(0.5f);
            enemyPoint = CardManager.Instance.CalculatePoint().Item2;
        } while (enemyPoint < playerPoint);

        _currentState = Define.PlayState.None;
        GameManager.Instance.Player.CurrentState = Define.PlayState.Draw;
        GameManager.Instance.IsPlayerTurn = true;
        GameManager.Instance.CheckState();
    }
}