using TMPro;
using UnityEngine;

public class UI_GameClearCanvas : MonoBehaviour
{
    Canvas _gameClearCanvas;

    TextMeshProUGUI _gameClearWinStreak;

    void Awake()
    {
        _gameClearCanvas = GetComponent<Canvas>();

        _gameClearWinStreak = FindAnyObjectByType<UI_GameClearWinstreak>().gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UIManager.Instance.toggleGameClearCanvasAction += ToggleGameClear;
        UIManager.Instance.disableAction += Disable;
    }

    public void ToggleGameClear()
    {
        _gameClearWinStreak.text = $"Winstreak: {DataManager.Instance.GameData.winStreak}";
        if (DataManager.Instance.GameData.highestWinstreak < DataManager.Instance.GameData.winStreak)
        {
            DataManager.Instance.GameData.highestWinstreak = DataManager.Instance.GameData.winStreak;
        }
        _gameClearCanvas.enabled = !_gameClearCanvas.enabled;
    }

    public void Disable()
    {
        _gameClearCanvas.enabled = false;
    }
}