using UnityEngine;
using System.Collections.Generic;
using System;

public class CardManager : MonoBehaviour
{

    static CardManager _instance;
    public static CardManager Instance => _instance;
    List<Card> _deck = new List<Card>();
    List<Card> _playerDeck = new List<Card>();
    List<Card> _enemyDeck = new List<Card>();
    List<Card> _usedDeck = new List<Card>();
    List<GameObject> _cardsOnTable = new List<GameObject>();

    //테스트용
    [SerializeField] Transform _playerPos, _enemyPos;
    private void Awake()
    {
        _instance = this;
        Init();
    }
    private void Start()
    {
        FirstDealing();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.IsPlayerTurn = !GameManager.Instance.IsPlayerTurn;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dealing();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            FirstDealing();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Tuple<int, int> test = CalculatePoint();
            Debug.Log($"Enemy Point {test.Item1} Player Point {test.Item2}");
        }
    }
    void Init() //초기화 시 덱에 모든 카드 넣기
    {
        ResetDeck();
    }
    public void FirstDealing() //매턴 시작마다 상대와 플레이어에게 2장식 카드 제공
    {
        _enemyDeck.Clear();
        _playerDeck.Clear();
        DiscardCards();
        if (_deck.Count < 4) //덱에 남은 카드가 3장 이하일 시 덱 초기화
        {
            ResetDeck();
        }
        for (int i = 0; i < 2; i++)
        {
            int deckIndex = UnityEngine.Random.Range(0, _deck.Count);
            SpawnCardObjects(false, _deck[deckIndex].gameObject);
            _enemyDeck.Add(_deck[deckIndex]); //덱에서 랜덤하게 카드를 뽑아 상대 덱에 넣기
            _usedDeck.Add(_deck[deckIndex]); //뽑힌 카드를 사용된 카드 리스트에 넣기
            _deck.RemoveAt(deckIndex); //뽑힌 카드를 덱에서 제거
            deckIndex = UnityEngine.Random.Range(0, _deck.Count); //위 상대 딜링과 동일
            SpawnCardObjects(true, _deck[deckIndex].gameObject);
            _playerDeck.Add(_deck[deckIndex]);
            _usedDeck.Add(_deck[deckIndex]);
            _deck.RemoveAt(deckIndex);
        }
        //GameManager.Instance.CheckState();
    }

    public void Dealing() //덱에서 한 장을 뽑아 플레이어 또는 상대 덱에 넣기
    {
        if (_deck.Count == 0)
        {
            GameManager.Instance.IsPlayerTurn = !GameManager.Instance.IsPlayerTurn; //덱에 남은 카드가 없을 시 턴 종료
            //GameManager.Instance.CheckState(); //턴 종료
            return;
        }
        if (GameManager.Instance.IsPlayerTurn) //현재 플레이어 턴일시
        {
            int deckIndex = UnityEngine.Random.Range(0, _deck.Count);
            _playerDeck.Add(_deck[deckIndex]);
            SpawnCardObjects(true, _deck[deckIndex].gameObject);
            _usedDeck.Add(_deck[deckIndex]);
            _deck.RemoveAt(deckIndex);
        }
        else //현재 상대 턴일 시
        {
            int deckIndex = UnityEngine.Random.Range(0, _deck.Count);
            _enemyDeck.Add(_deck[deckIndex]);
            SpawnCardObjects(false, _deck[deckIndex].gameObject);
            _usedDeck.Add(_deck[deckIndex]);
            _deck.RemoveAt(deckIndex);
        }
    }

    public Tuple<int, int> CalculatePoint()
    {
        int enemyPoint = 0;
        foreach (Card card in _enemyDeck)//상대 덱에 있는 카드의 값을 모두 계산
        {
            enemyPoint += card.Number;
        }
        int playerPoint = 0;
        foreach (Card card in _playerDeck)//플레이어 덱에 있는 카드의 값을 모두 계산
        {
            playerPoint += card.Number;
        }
        GameManager.Instance.Enemy.ChooseDecision(enemyPoint);
        //UIManager.Instance.ShowDraw();
        return Tuple.Create(enemyPoint, playerPoint);
    }

    public void ResetDeck() //덱을 다시 모든 카드가 있는 상태로 초기화
    {
        _deck.Clear();
        _usedDeck.Clear();
        for (int i = 0; i < 52; i++)
        {
            _deck.Add(Resources.Load<Card>($"Cards/{i}"));
        }
    }

    void SpawnCardObjects(bool isPlayerCard, GameObject card) //카드 오브젝트 스폰해서 테이블에 놓기
    {
        GameObject dealtCard = Instantiate(card);
        _cardsOnTable.Add(dealtCard);
        if (isPlayerCard)
        {
            dealtCard.transform.SetParent(_playerPos);
            Debug.Log(_playerPos.position + new Vector3(0.5f * (_playerPos.childCount - 1), 0f, 0f));
            dealtCard.transform.position = _playerPos.position + new Vector3(0.5f * (_playerPos.childCount-1), 0f, 0f);
            dealtCard.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        }
        else
        {
            dealtCard.transform.SetParent(_enemyPos);
            Debug.Log(_enemyPos.position + new Vector3(0.5f * (_enemyPos.childCount - 1), 0f, 0f));
            dealtCard.transform.position = _enemyPos.position + new Vector3(0.5f * (_enemyPos.childCount-1), 0f, 0f);
            dealtCard.transform.rotation = Quaternion.Euler(90f, 0, 0);
        }
    }

    void DiscardCards() //현재 테이블에 있는 모든 카드 제거
    {
        foreach (GameObject card in _cardsOnTable)
        {
            Destroy(card);
        }
        _cardsOnTable.Clear();

    }

}
