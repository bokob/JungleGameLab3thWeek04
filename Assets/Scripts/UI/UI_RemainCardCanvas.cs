using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UI_RemainCardCanvas : MonoBehaviour
{
    Canvas _remainCardCanvas;

    [SerializeField]
    TextMeshProUGUI[] _remainTexts;
    Dictionary<int, int> _remainCountDict = new Dictionary<int, int>(); // 0: ace, ..., 9: 10, 10: jack, 11: queen, 12: king

    void Awake()
    {
        _remainCardCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        _remainTexts = GetComponentsInChildren<TextMeshProUGUI>();
        InputManager.Instance.toggleUsedCardAction += ToggleUsedCard;
        UIManager.Instance.updateUsedCardUIAction += UpdateUsedCardUI;
        UIManager.Instance.disableAction += Disable;
    }

    void Init()
    {
        for (int i = 0; i < _remainTexts.Length; i++)
            _remainCountDict[i] = 0;
    }

    public void UpdateUsedCardUI()
    {
        Init();

        List<Card> deck = CardManager.Instance.Deck;
        foreach(Card card in deck)
        {
            int idx = (int)card.CardType;
            _remainCountDict[idx]++;
        }

        for(int idx=0; idx < _remainTexts.Length; idx++)
        {
            string cardName = ((Define.CardType)idx).ToString();
            _remainTexts[idx].text = $"{cardName} \n {_remainCountDict[idx]}";
        }
    }

    // 버린 카드 토글 (추후에 마우스 Hover에 의해 나오게 하기)
    public void ToggleUsedCard()
    {
        Debug.Log("사용한 카드 토글");
        _remainCardCanvas.enabled = !_remainCardCanvas.enabled;
    }

    public void Disable()
    {
        _remainCardCanvas.enabled = false;
    }
}