using Bmc;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    public Define.PlayState CurrentState { get => _currentState; set => _currentState = value; }

    public const int blackJack = 21;

    // Revolver _revolver;
    bool _isLive;
    Define.PlayState _currentState;

    // up, spot, down일 때 뭐 할지 결정 (일단 에이스는 1점으로 계산)
    public void ChooseDecision()
    {
        // 현재 덱 정보와 플레이어의 현재 덱 정보 가져옴
        List<Card> deck = CardManager.Instance.Deck;
        List<Card> playerDeck = CardManager.Instance.PlayerDeck;

        // 두 정보에서 평균값 산정
        int cnt = deck.Count + playerDeck.Count;
        int point = 0;
        foreach(Card card in deck)
        {
            point += card.Number;
        }
        foreach(Card card in playerDeck)
        {
            point += card.Number;
        }

        // 플레이어의 패를 평균값으로 가정하고 21보다 큰 지, 작은 지에 따라서 판단
        // up, spot, down
        int avg = point / cnt; // 버림
        if(avg > blackJack) // up
        {
            //GameManager.Instance.EnemyDecision = Define.EnemyDecision.Up;
        }
        else if(avg < blackJack) // down
        {
            //GameManager.Instance.EnemyDecision = Define.EnemyDecision.Down;
        }
        else // spot
        {
            //GameManager.Instance.EnemyDecision = Define.EnemyDecision.BlackJack;
        }
    }

    // 카드 뽑기
    public IEnumerator Play()
    {
        int enemyPoint = CardManager.Instance.CalculatePoint().Item1;
        int playerPoint = CardManager.Instance.CalculatePoint().Item2;
        Debug.Log("here");
        while (enemyPoint < playerPoint)
        {
            CardManager.Instance.Dealing();
            yield return new WaitForSeconds(0.5f);
            enemyPoint = CardManager.Instance.CalculatePoint().Item1;
        }

        _currentState = Define.PlayState.None;
        GameManager.Instance.Player.CurrentState = Define.PlayState.Draw;
        GameManager.Instance.IsPlayerTurn = true;
        GameManager.Instance.CheckState();
    }
}