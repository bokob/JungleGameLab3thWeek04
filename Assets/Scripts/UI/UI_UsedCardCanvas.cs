using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UI_UsedCardCanvas : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] _usedTexts;
    Dictionary<int, int> _usedCountDict = new Dictionary<int, int>(); // 0: ace, ..., 9: 10, 10: jack, 11: queen, 12: king

    void Start()
    {
        _usedTexts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    void Init()
    {
        for (int i = 0; i < _usedTexts.Length; i++)
            _usedCountDict[i] = 0;
    }

    public void UpdateUsedCardUI()
    {
        Init();

        List<Card> usedDeck = CardManager.Instance.UsedDeck;
        foreach(Card card in usedDeck)
        {
            int idx = (int)card.CardType;
            _usedCountDict[idx]++;
        }

        for(int idx=0; idx < _usedTexts.Length; idx++)
        {
            string cardName = ((Define.CardType)idx).ToString();
            _usedTexts[idx].text = $"{cardName} \n {_usedCountDict[idx]}";
        }
    }
}