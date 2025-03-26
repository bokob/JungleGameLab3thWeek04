using UnityEngine;
using UnityEngine.UI;

public class UI_DrawCanvas : MonoBehaviour
{
    Button _addBtn;
    Button _stopBtn;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        _addBtn = buttons[0];
        _stopBtn = buttons[1];
        _addBtn.onClick.AddListener(() => UIManager.Instance.OnAddCardBtn());
        _stopBtn.onClick.AddListener(() => UIManager.Instance.OnStopCardBtn());
    }
}