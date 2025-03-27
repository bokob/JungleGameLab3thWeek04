using UnityEngine;

/// <summary>
/// 카드 정보를 담고 있는 클래스
/// </summary>
public class Card : MonoBehaviour
{
    public Define.CardType CardType => _cardType;   // 카드 종류(Ace, Two, ..., King)
    public int Number => _number;                   // 카드 포인트
    public bool IsAce => _isAce;                    // 에이스 카드인지 체크

    [SerializeField] Define.CardType _cardType;
    [SerializeField] int _number;
    [SerializeField] bool _isAce;
}