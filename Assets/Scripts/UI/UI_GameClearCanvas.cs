using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class UI_GameClearCanvas : MonoBehaviour
{
    Canvas _gameClearCanvas;

    TextMeshProUGUI _gameClearWinStreak;

    GameObject _ending;
    VideoClip _endingClip;

    void Awake()
    {
        _gameClearCanvas = GetComponent<Canvas>();

        _gameClearWinStreak = FindAnyObjectByType<UI_GameClearWinstreak>().gameObject.GetComponent<TextMeshProUGUI>();

        // 영상
        UI_EndingCanvas _endingCanvas = FindAnyObjectByType<UI_EndingCanvas>();
        if(_endingCanvas != null)
        {
            _ending = _endingCanvas.gameObject;
            _endingClip = _ending.GetComponentInChildren<VideoPlayer>().clip;
            _ending.SetActive(false);
        }
    }

    void Start()
    {
        UIManager.Instance.toggleGameClearCanvasAction += ToggleGameClear;
        UIManager.Instance.disableAction += Disable;
    }

    public void ToggleGameClear()
    {
        if(DataManager.Instance.GameData.winStreak == 5)
        {
            _ending.SetActive(true);
        }

        _gameClearWinStreak.text = $"Winstreak: {DataManager.Instance.GameData.winStreak}";
        _gameClearCanvas.enabled = !_gameClearCanvas.enabled;
    }

    public void Disable()
    {
        _gameClearCanvas.enabled = false;
    }
}