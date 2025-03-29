using UnityEngine;

public class UI_GameOverCanvas : MonoBehaviour
{
    Canvas _gameOverCanvas;

    void Awake()
    {
        _gameOverCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        UIManager.Instance.toggleGameOverCanvasAction += ToggleGameOver;
        UIManager.Instance.disableAction += Disable;
    }

    public void ToggleGameOver()
    {
        _gameOverCanvas.enabled = !_gameOverCanvas.enabled;
    }

    public void Disable()
    {
        _gameOverCanvas.enabled = false;
    }
}