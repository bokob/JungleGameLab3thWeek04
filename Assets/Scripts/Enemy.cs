using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
public class Enemy : MonoBehaviour
{
    public Define.PlayState CurrentState { get => _currentState; set => _currentState = value; }
    public Revolver Revolver => _revolver;

    Revolver _revolver;
    Define.PlayState _currentState;

    int GuessNumber = 20; // 해당 숫자가 될 때까지 카드 뽑음 (2장일 때 예측 방지)

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
        float avg = (float)point / cnt;
        float predictScore = avg * playerDeck.Count;

        float varianceScore = 0;

        foreach (Card card in deck)
        {
            float differencefromAverage = (card.Number - avg);
            varianceScore += differencefromAverage * differencefromAverage;
        }

        foreach (Card card in playerDeck)
        {
            float differencefromAverage = (card.Number - avg);
            varianceScore += (differencefromAverage * differencefromAverage);
        }

        varianceScore /= (playerDeck.Count + deck.Count);
        float standardDeviation = math.sqrt(varianceScore);

        if (playerDeck.Count == 2)
        {
            GameManager.Instance.EnemyGuess = Define.Guess.Down;
        }
        else if (playerDeck.Count > 4)
        {
            GameManager.Instance.EnemyGuess = Define.Guess.Up;
        }
        else
        {
            if (standardDeviation > 2.8)
            {
                Debug.Log("편차가 너무 큼");
                int randomValue = UnityEngine.Random.Range(0, 2);
                GameManager.Instance.EnemyGuess = (randomValue == 1) ? Define.Guess.Up : Define.Guess.Down;
            }
            else if (standardDeviation > 1.8)
            {
                Debug.Log("편차가 적당함");
                int ans = -1;
                if (predictScore > CardManager.Instance.BlackJack) // up
                {
                    ans = 0;
                }
                else if (predictScore < CardManager.Instance.BlackJack) // down
                {
                    ans = 1;
                }

                int randomValue = UnityEngine.Random.Range(0, 5);
                if (ans == -1)
                {
                    GameManager.Instance.EnemyGuess = Define.Guess.BlackJack;
                }
                else
                {
                    if (ans == 0)
                    {
                        if (randomValue == 4)
                        {
                            Debug.Log("운나쁘게 다운으로 해석함");
                            GameManager.Instance.EnemyGuess = Define.Guess.Down;
                        }
                        else
                        {
                            Debug.Log("바르게 업으로 해석함");
                            GameManager.Instance.EnemyGuess = Define.Guess.Up;
                        }
                    }
                    else
                    {
                        if (randomValue == 4)
                        {
                            Debug.Log("운나쁘게 업으로 해석함");
                            GameManager.Instance.EnemyGuess = Define.Guess.Up;
                        }
                        else
                        {
                            Debug.Log("바르게 다운으로 해석함");
                            GameManager.Instance.EnemyGuess = Define.Guess.Down;
                        }
                    }
                }

            }
            else
            {
                Debug.Log("편차 적음");
                if (predictScore > CardManager.Instance.BlackJack) // up
                {
                    GameManager.Instance.EnemyGuess = Define.Guess.Up;
                }
                else if (predictScore < CardManager.Instance.BlackJack) // down
                {
                    GameManager.Instance.EnemyGuess = Define.Guess.Down;
                }
                else
                {
                    GameManager.Instance.EnemyGuess = Define.Guess.BlackJack;
                }
            }
        }
    }

    // 카드 뽑기
    public IEnumerator DrawCoroutine()
    {
        int enemyPoint = CardManager.Instance.CalculatePoint().Item2;

        while (enemyPoint < GuessNumber)
        {
            CardManager.Instance.DrawCard();
            yield return new WaitForSeconds(0.5f);
            enemyPoint = CardManager.Instance.CalculatePoint().Item2;
        }

        _currentState = Define.PlayState.None;
        GameManager.Instance.Player.CurrentState = Define.PlayState.Draw;
        GameManager.Instance.IsPlayerTurn = true;
        GameManager.Instance.CheckState();
    }
}