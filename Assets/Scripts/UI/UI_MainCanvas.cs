using UnityEngine;
using UnityEngine.UI;

public class UI_MainCanvas : MonoBehaviour
{
    Canvas _mainCanvas;
    void Awake()
    {
        _mainCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        UIManager.Instance.toggleMainCanvasAction += ToggleMain;
        UIManager.Instance.disableAction += Disable;
    }

    // 메인 캔버스 토글
    public void ToggleMain()
    {
        UIManager.Instance.DisableAllCanvas();
        _mainCanvas.enabled = !_mainCanvas.enabled;
    }

    public void Disable()
    {
        _mainCanvas.enabled = false;
    }
}