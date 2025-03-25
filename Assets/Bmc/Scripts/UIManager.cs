using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance => _instance;

    Button _upBtn;
    Button _downBtn;
    Button _spotBtn;
    Button _addBtn;
    Button _stopBtn;
    Button _usedCardBtn;
    Canvas _usedCardCanvas;
    TextMeshProUGUI _gameResult;

    void Awake()
    {
        
    }

    // 모든 UI 컴포넌트 찾고, 등록할 것이 있으면 등록
    public void Init()
    {
    }

    // 버린 카드 보여주기
    public void ShowUsedCard()
    {
    }

    public void ShowCheckBtn() 
    { 
    }

    public void ShowAddCardBtn() 
    { 
    }

    public void ShowPoint() 
    { 
    }

    public void ShowGameResult()
    {
    }

    void OnCheckBtn(int idx) 
    { 
    }
    
    void OnAddCardBtn() 
    { 
    }
    void OnStopCardBtn() 
    { 
    }
}
