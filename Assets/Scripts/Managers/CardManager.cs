using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

/// <summary>
/// 카드 관리 매니저
/// </summary>
public class CardManager : MonoBehaviour
{
    public static CardManager Instance => _instance;

    static CardManager _instance;

    #region 덱
    public List<Card> Deck => _deck;
    public List<Card> PlayerDeck => _playerDeck;
    public List<Card> EnemyDeck => _enemyDeck;
    public List<Card> UsedDeck => _usedDeck;

    List<Card> _deck = new List<Card>();
    List<Card> _playerDeck = new List<Card>();
    List<Card> _enemyDeck = new List<Card>();
    List<Card> _usedDeck = new List<Card>();
    #endregion

    #region 카드
    public int BlackJack { get; } = 21;                         // 블랙잭 값
    int _drawLimit = 6;                                         // 한 턴에 뽑을 수 있는 카드 수
    float _cardMoveSpeed = 5;                                   // 카드 움직임 속도
    float _cardSpace = 0.05f;                                   // 카드 사이 간격
    [SerializeField] Transform _playerCardPos;                  // 플레이어 카드 배치 시작 위치
    [SerializeField] Transform _enemyCardPos;                   // 적 카드 배치 시작 위치
    List<GameObject> _cardsOnTable = new List<GameObject>();    // 테이블에 있는 카드 리스트
    #endregion

    void Awake()
    {
        _instance = this;
        Init();
    }

    //초기화 시 덱에 모든 카드 넣기
    void Init()
    {
        ResetDeck();
    }

    // 매턴 시작마다 상대와 플레이어에게 2장식 카드 제공
    public void StartTurn()
    {
        // 사용된 카드를 UsedDeck에 넣고 플레이어와 상대 덱을 비우기
        foreach (Card card in _playerDeck)
        {
            _usedDeck.Add(card);
        }
        foreach (Card card in _enemyDeck)
        {
            _usedDeck.Add(card);
        }
        _playerDeck.Clear();
        _enemyDeck.Clear();
        
        // 테이블 위 카드 처분
        DiscardCards();
        UIManager.Instance.UIUsedCardCanvas.UpdateUsedCardUI(); // UsedCard UI 업데이트

        // 덱에 남은 카드가 3장 이하일 시 덱 초기화
        if (_deck.Count < 4)
        {
            ResetDeck();
        }

        // 턴 시작시 카드 2장씩 배부
        for (int i = 0; i < 2; i++)
        {
            // 플레이어 카드 뽑기
            int deckIndex = UnityEngine.Random.Range(0, _deck.Count);
            StartCoroutine(PutCardOnTableCoroutine(_playerCardPos, true, _deck[deckIndex].gameObject));
            _playerDeck.Add(_deck[deckIndex]);
            _deck.RemoveAt(deckIndex);
            SoundManager.Instance.PlayEffect("Card_Sound");

            // 적 카드 뽑기
            deckIndex = UnityEngine.Random.Range(0, _deck.Count);
            StartCoroutine(PutCardOnTableCoroutine(_enemyCardPos, false, _deck[deckIndex].gameObject));
            _enemyDeck.Add(_deck[deckIndex]);   //덱에서 랜덤하게 카드를 뽑아 상대 덱에 넣기
            _deck.RemoveAt(deckIndex);          //뽑힌 카드를 덱에서 제거
            SoundManager.Instance.PlayEffect("Card_Sound");
        }

        GameManager.Instance.Enemy.CurrentState = Define.PlayState.Draw; // 적이 선공이므로 적의 상태를 Draw로 변경
        GameManager.Instance.CheckState();                               // 상황에 맞는 판단(적이 카드 뽑음)
    }

    public void DrawCard() // 덱에서 한 장을 뽑아 플레이어 또는 상대 덱에 넣기
    {
        if (_deck.Count == 0)
        {
            GameManager.Instance.IsPlayerTurn = !GameManager.Instance.IsPlayerTurn; // 덱에 남은 카드가 없을 시 턴 종료
            GameManager.Instance.CheckState();                                      // 턴 종료
            return;
        }
        
        /* 추후에 더 줄이기 */
        int deckIndex = UnityEngine.Random.Range(0, _deck.Count);
        SoundManager.Instance.PlayEffect("Card_Sound");
        if (GameManager.Instance.IsPlayerTurn && _playerDeck.Count < _drawLimit) //현재 플레이어 턴일시
        {
            _playerDeck.Add(_deck[deckIndex]);
            StartCoroutine(PutCardOnTableCoroutine(_playerCardPos, true, _deck[deckIndex].gameObject));
            _deck.RemoveAt(deckIndex);
        }
        else if (!GameManager.Instance.IsPlayerTurn && _enemyDeck.Count < _drawLimit)//현재 상대 턴일 시
        {
            _enemyDeck.Add(_deck[deckIndex]);
            StartCoroutine(PutCardOnTableCoroutine(_enemyCardPos, false, _deck[deckIndex].gameObject));
            _deck.RemoveAt(deckIndex);
        }
    }

    // 플레이어와 적의 카드 점수 계산
    public Tuple<int, int> CalculatePoint() // Item1: 플레이어, Item2: 적
    {
        int playerPoint = 0, enemyPoint = 0;
        foreach (Card card in _playerDeck)      //플레이어 덱에 있는 카드의 값을 모두 계산
            playerPoint += card.Number;
        foreach (Card card in _enemyDeck)       //상대 덱에 있는 카드의 값을 모두 계산
            enemyPoint += card.Number;

        return Tuple.Create(playerPoint, enemyPoint);
    }

    // 덱을 다시 모든 카드가 있는 상태로 초기화
    public void ResetDeck()
    {
        _deck.Clear();
        _usedDeck.Clear();

        for (int i = 0; i < 52; i++)
            _deck.Add(Resources.Load<Card>($"Cards/{i}"));
    }

    // 카드 오브젝트 스폰해서 테이블에 놓기
    IEnumerator PutCardOnTableCoroutine(Transform cardPosTransform, bool isPlayerCard, GameObject card)
    {
        // 테이블에 카드 추가
        GameObject dealtCard = Instantiate(card, new Vector3(0, 1f, 0), Quaternion.Euler(90, 0, 90f)); // 덱 위치에서 소환
        _cardsOnTable.Add(dealtCard);

        // 플레이어, 적에 따라 위치 조정
        int offset = (isPlayerCard) ? -1 : 1;
        dealtCard.transform.SetParent(cardPosTransform);
        Vector3 targetPosition = cardPosTransform.position + new Vector3(0f, 0.0002f * (cardPosTransform.childCount - 1), offset * _cardSpace * (cardPosTransform.childCount - 1));
        while (Vector3.Distance(dealtCard.transform.position, targetPosition) > 0.1f)
        {
            dealtCard.transform.position = Vector3.MoveTowards(dealtCard.transform.position, targetPosition, _cardMoveSpeed * Time.deltaTime);
            yield return null;
        }
        dealtCard.transform.position = targetPosition;
        dealtCard.transform.rotation = Quaternion.Euler(offset * 90f, 0f, offset * -90f);
    }

    // 현재 테이블에 있는 모든 카드 제거
    void DiscardCards()
    {
        foreach (GameObject card in _cardsOnTable)
        {
            card.transform.SetParent(null);
            Destroy(card);
        }
        _cardsOnTable.Clear();
    }

    // Guess Result 시, Enemy 카드 뒤집기
    public void FlipCards()
    {
        for(int i = 0; i < _enemyCardPos.childCount; i++)
        {
            _enemyCardPos.GetChild(i).transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }
    }
}