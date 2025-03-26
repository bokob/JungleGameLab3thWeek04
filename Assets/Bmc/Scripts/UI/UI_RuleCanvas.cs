using UnityEngine;
using UnityEngine.UI;

public class UI_RuleCanvas : MonoBehaviour
{
    Button _closeBtn;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        _closeBtn = buttons[0];
        _closeBtn.onClick.AddListener(() => UIManager.Instance.OnRuleCloseBtn());
    }
}