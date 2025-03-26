using UnityEngine;
using UnityEngine.UI;

public class UI_MainCanvas : MonoBehaviour
{
    Button _usedCardBtn;
    Button _ruleBtn;
    Button _exitBtn;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        _usedCardBtn = buttons[0];
        _ruleBtn = buttons[1];
        _exitBtn = buttons[2];

        _usedCardBtn.onClick.AddListener(() => UIManager.Instance.ShowUsedCard());
        _ruleBtn.onClick.AddListener(() => UIManager.Instance.ShowRule());
        _exitBtn.onClick.AddListener(() => UIManager.Instance.OnExitBtn());
    }
}