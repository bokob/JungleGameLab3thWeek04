using UnityEngine;

public class UI_RuleCanvas : MonoBehaviour
{
    Canvas _ruleCanvas;

    void Awake()
    {
        _ruleCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        InputManager.Instance.toggleRuleAction += ToggleRule;
    }

    // 룰 토글
    public void ToggleRule()
    {
        Debug.Log("룰 토글");

        int timeScale = (int)Time.timeScale;
        timeScale = timeScale == 0 ? 1 : 0;
        Time.timeScale = (float)timeScale;

        _ruleCanvas.enabled = !_ruleCanvas.enabled;
        GameManager.Instance.CameraController.enabled = !_ruleCanvas.enabled;
    }
}