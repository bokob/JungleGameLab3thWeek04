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
        Init();
    }

    void Init()
    {
        for (int i = 0; i < _usedTexts.Length; i++)
            _usedCountDict[i] = 0;
    }

    // 테스트 코드
    public void DebugDict()
    {
        foreach (var pair in _usedCountDict)
        {
            Debug.Log($"{pair.Key} : {pair.Value}");
        }
    }

    public void UpdateUsedCardUI()
    {
        List<Card> usedDeck = CardManager.Instance.UsedDeck;
        foreach(Card card in usedDeck)
        {
            int idx = (int)card.CardType;
            _usedCountDict[idx]++;
        }

        for(int i=0; i < _usedTexts.Length; i++)
        {
            _usedTexts[i].text = _usedCountDict[i].ToString();
        }
    }
}