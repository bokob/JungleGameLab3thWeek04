using UnityEngine;
using UnityEngine.UI;

public class UI_GuessCanvas : MonoBehaviour
{
    Canvas _guessCanvas;

    void Awake()
    {
        _guessCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        UIManager.Instance.toggleGuessTextAction += ToggleGuessText;
        UIManager.Instance.disableAction += Disable;
    }

    // 추측 문구 토글(up, spot, down)
    void ToggleGuessText()
    {
        _guessCanvas.enabled = !_guessCanvas.enabled;
    }

    public void Disable()
    {
        _guessCanvas.enabled = false;
    }
}