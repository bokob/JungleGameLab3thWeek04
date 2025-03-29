using UnityEngine;
using UnityEngine.UI;

public class UI_DrawCanvas : MonoBehaviour
{
    Canvas _drawCanvas;

    void Awake()
    {
        _drawCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        UIManager.Instance.toggleDrawCanvasAction += ToggleDraw;
        UIManager.Instance.disableAction += Disable;
    }

    // 드로우 창 보여주기
    public void ToggleDraw()
    {
        _drawCanvas.enabled = !_drawCanvas.enabled;
        Debug.Log("드로우 토글");
    }

    public void Disable()
    {
        _drawCanvas.enabled = false;
    }
}