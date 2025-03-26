using UnityEngine;
using UnityEngine.UI;

public class UI_CheckCanvas : MonoBehaviour
{
    Button _upBtn;
    Button _spotBtn;
    Button _downBtn;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        _upBtn = buttons[0];
        _spotBtn = buttons[1];
        _downBtn = buttons[2];

        _upBtn.onClick.AddListener(() => UIManager.Instance.OnCheckBtn(1));
        _spotBtn.onClick.AddListener(() => UIManager.Instance.OnCheckBtn(1));
        _downBtn.onClick.AddListener(() => UIManager.Instance.OnCheckBtn(2));
    }
}