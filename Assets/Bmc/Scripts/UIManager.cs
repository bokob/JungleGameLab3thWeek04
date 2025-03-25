using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance => _instance;

    #region Canvas
    Canvas _mainCanvas;
    Canvas _checkCanvas;
    Canvas _resultCanvas;
    Canvas _ruleCanvas;
    Canvas _drawCanvas;
    Canvas _usedCardCanvas;
    #endregion

    #region Button
    Button _upBtn;
    Button _downBtn;
    Button _spotBtn;
    Button _addBtn;
    Button _stopBtn;
    Button _usedCardBtn;
    #endregion

    #region TextMeshPro
    TextMeshProUGUI _gameResult;

    TextMeshProUGUI _aceText;
    TextMeshProUGUI _twoText;
    TextMeshProUGUI _threeText;
    TextMeshProUGUI _fourText;
    TextMeshProUGUI _fiveText;
    TextMeshProUGUI _sixText;
    TextMeshProUGUI _sevenText;
    TextMeshProUGUI _eightText;
    TextMeshProUGUI _nineText;
    TextMeshProUGUI _tenText;
    TextMeshProUGUI _jackText;
    TextMeshProUGUI _queenText;
    TextMeshProUGUI _kingText;
    #endregion

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Init();
    }

    // 모든 UI 컴포넌트 찾고, 등록할 것이 있으면 등록
    public void Init()
    {
        //Canvas[] canvases = transform.GetComponentsInChildren<Canvas>();
        //for (int i = 1; i < canvases.Length; i++)
        //{
        //}
    }

    // 버린 카드 보여주기
    public void ShowUsedCard()
    {
    }

    // 체크 버튼들 보여주기(up, down, spot)
    public void ShowCheckBtn() 
    { 
    }

    // 카드 뽑기 or 멈춤 버튼 보여주기
    public void ShowAddCardBtn() 
    { 
    }

    // 점수 보여주기
    public void ShowPoint() 
    { 
    }

    // 게임 결과 보여주기
    public void ShowGameResult()
    {
    }

    // 체크 버튼 중 하나 동작 (up: 1, down:2, spot: 3)
    void OnCheckBtn(int idx) 
    { 
    }

    // 카드 뽑기 버튼 동작
    void OnAddCardBtn() 
    {
        
    }

    // 카드 뽑기 멈춤 버튼 동작
    void OnStopCardBtn() 
    { 
    }
}