using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Build.Content;

public class CardManager : MonoBehaviour
{

    static CardManager _instance;
    public static CardManager Instance => _instance;
    public List<Card> deck;
    public List<Card> playerDeck;
    public List<Card> enemyDeck;
    public List<Card> usedDeck;

    private void Awake()
    {
        _instance = this;
        Init();
    }
    private void Start()
    {
        //FirstDealing();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    GameManager.Instance.IsPlayerTurn = !GameManager.Instance.IsPlayerTurn;
        //}

        //if (Input.GetKeyDown(KeyCode.Space)){
        //    Dealing();
        //}
    }
    public void Init() //초기화 시 덱에 모든 카드 넣기
    {
        ResetDeck();
    }
    public void FirstDealing() //매턴 시작마다 상대와 플레이어에게 2장식 카드 제공
    {
        if (deck.Count < 4) //덱에 남은 카드가 3장 이하일 시 덱 초기화
        {
            ResetDeck();
        }
        for (int i = 0; i < 2; i++)
        {
            int deckIndex = Random.Range(0, deck.Count);
            enemyDeck.Add(deck[deckIndex]); //덱에서 랜덤하게 카드를 뽑아 상대 덱에 넣기
            usedDeck.Add(deck[deckIndex]); //뽑힌 카드를 사용된 카드 리스트에 넣기
            deck.RemoveAt(deckIndex); //뽑힌 카드를 덱에서 제거
            deckIndex = Random.Range(0, deck.Count); //위 상대 딜링과 동일
            playerDeck.Add(deck[deckIndex]);
            usedDeck.Add(deck[deckIndex]);
            deck.RemoveAt(deckIndex);
        }
        GameManager.Instance.CheckState();
    }

    public void Dealing() //덱에서 한 장을 뽑아 플레이어 또는 상대 덱에 넣기
    {
        if (deck.Count == 0)
        {
            GameManager.Instance.IsPlayerTurn = !GameManager.Instance.IsPlayerTurn; //덱에 남은 카드가 없을 시 턴 종료
            GameManager.Instance.CheckState(); //턴 종료
            return;
        }
        if (GameManager.Instance.IsPlayerTurn) //현재 플레이어 턴일시
        {
            int deckIndex = Random.Range(0, deck.Count);
            playerDeck.Add(deck[deckIndex]);
            usedDeck.Add(deck[deckIndex]);
            deck.RemoveAt(deckIndex);
        }
        else //현재 상대 턴일 시
        {
            int deckIndex = Random.Range(0, deck.Count);
            enemyDeck.Add(deck[deckIndex]);
            usedDeck.Add(deck[deckIndex]);
            deck.RemoveAt(deckIndex);
        }
    }

    public (int, int) CalculatePoint()
    {
        int enemyPoint = 0;
        foreach (Card card in enemyDeck)//상대 덱에 있는 카드의 값을 모두 계산
        {
            enemyPoint += card.number;
        }
        int playerPoint = 0;
        foreach (Card card in playerDeck)//플레이어 덱에 있는 카드의 값을 모두 계산
        {
            playerPoint += card.number;
        }
        enemyDeck.Clear();
        playerDeck.Clear();
        return (enemyPoint, playerPoint);
    }

    public void ResetDeck() //덱을 다시 모든 카드가 있는 상태로 초기화
    {
        deck.Clear();
        usedDeck.Clear();
        for (int i = 0; i < 52; i++)
        {
            deck.Add(Resources.Load<Card>($"Cards/{i}"));
        }
    }

}
