using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameTitleCanvas : MonoBehaviour
{
    Canvas _gameTitleCanvas;

    Image _crownImage;  // 왕관 이미지
    TextMeshProUGUI _highestWinStreak;
    TextMeshProUGUI _gameStartWinStreak;

    void Awake()
    {
        _gameTitleCanvas = GetComponent<Canvas>();

        _crownImage = FindAnyObjectByType<UI_CrownImage>().gameObject.GetComponent<Image>();
        _highestWinStreak = FindAnyObjectByType<UI_HighestWinStreak>().gameObject.GetComponent<TextMeshProUGUI>();
        _gameStartWinStreak = FindAnyObjectByType<UI_GameStartWinStreak>().gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UIManager.Instance.toggleGameTitleAction += ToggleGameTitleCanvas;
        UIManager.Instance.disableAction += Disable;
    }

    // 게임 타이틀 UI
    public void ToggleGameTitleCanvas()
    {
        _gameTitleCanvas.enabled = true;

        int highestWinstreak = PlayerPrefs.GetInt("HighestWinstreak");
        int winStreak = PlayerPrefs.GetInt("Winstreak");

        if (highestWinstreak >= 20)
            _crownImage.enabled = true;

        _highestWinStreak.text = $"Highest Winstreak: {highestWinstreak}";
        _gameStartWinStreak.text = $"Winstreak: {winStreak}";

        Debug.LogWarning("게임 타이틀 활성화");
    }

    public void Disable()
    {
        _gameTitleCanvas.enabled = false;
    }
}