using UnityEngine;
using Bmc;
public class Card : MonoBehaviour
{
    [SerializeField] int _number;
    public int Number => _number; //카드 포인트
    [SerializeField] bool _isAce;
    public bool IsAce => _isAce; //에이스 카드인지 체크
    [SerializeField] Define.CardType _cardType;
    public Define.CardType CardType => _cardType;


}
