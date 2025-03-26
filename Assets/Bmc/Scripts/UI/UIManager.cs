using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance => _instance;

    #region Canvas
    Canvas _mainCanvas;
    Canvas _checkCanvas;
    Canvas _checkResultCanvas;
    Canvas _ruleCanvas;
    Canvas _drawCanvas;
    Canvas _usedCardCanvas;
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ShowDraw();
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            ShowMain();
        }
    }

    // 모든 UI 컴포넌트 찾고, 등록할 것이 있으면 등록
    public void Init()
    {
        _mainCanvas = FindAnyObjectByType<UI_MainCanvas>().gameObject.GetComponent<Canvas>();
        _checkCanvas = FindAnyObjectByType<UI_CheckCanvas>().gameObject.GetComponent<Canvas>();
        _checkResultCanvas = FindAnyObjectByType<UI_CheckResultCanvas>().gameObject.GetComponent<Canvas>();
        _ruleCanvas = FindAnyObjectByType<UI_RuleCanvas>().gameObject.GetComponent<Canvas>();
        _drawCanvas = FindAnyObjectByType<UI_DrawCanvas>().gameObject.GetComponent<Canvas>();
        _usedCardCanvas = FindAnyObjectByType<UI_UsedCardCanvas>().gameObject.GetComponent<Canvas>();
    }

    public void ShowMain()
    {
        DisableAllCanvas();
        _mainCanvas.enabled = true;
    }

    public void DisableAllCanvas()
    {
        _mainCanvas.enabled = false;
        _checkCanvas.enabled = false;
        _checkResultCanvas.enabled = false;
        _ruleCanvas.enabled = false;
        _drawCanvas.enabled = false;
        _usedCardCanvas.enabled = false;
    }

    // 게임 종료 버튼 동작
    public void OnExitBtn()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    #region 드로우 창
    // Draw Btn, Stop Btn 보여주기
    public void ShowDraw()
    {
        DisableAllCanvas();
        _drawCanvas.enabled = true;
    }

    // 카드 뽑기 버튼 동작
    public void OnAddCardBtn()
    {
        Debug.Log("카드 뽑기");
    }

    // 카드 뽑기 멈춤 버튼 동작
    public void OnStopCardBtn()
    {
        Debug.Log("카드 뽑기 멈춤");
        
        DisableAllCanvas();
        ShowCheckResult();
    }
    #endregion

    #region 사용한 카드
    // 버린 카드 보여주기 (추후에 마우스 Hover에 의해 나오게 하기)
    public void ShowUsedCard()
    {
        _usedCardCanvas.enabled = !_usedCardCanvas.enabled;
    }
    #endregion

    #region 체크
    // 체크 버튼들 보여주기(up, down, spot)
    public void ShowCheckBtn()
    {
        _checkCanvas.enabled = true;
    }

    // 체크 버튼 중 하나 동작 (up: 1, down:2, spot: 3)
    public void OnCheckBtn(int idx)
    {
        switch (idx)
        {
            case 1:
                Debug.Log("up");
                break;
            case 2:
                Debug.Log("down");
                break;
            case 3:
                Debug.Log("spot");
                break;
        }
        DisableAllCanvas();
        ShowCheckResult();
    }
    #endregion

    #region 체크 결과
    // 플레이어 점수(카드 숫자 총합) 보여주기 => 총 쏘는 연출로 넘어감
    public void ShowCheckResult()
    {
        DisableAllCanvas();
        _checkResultCanvas.enabled = true;
    }
    #endregion

    #region 룰
    // 룰 보여주기
    public void ShowRule()
    {
        _ruleCanvas.enabled = true;
    }

    // 룰 닫기
    public void OnRuleCloseBtn()
    {
        _ruleCanvas.enabled = false;
    }
    #endregion

    #region 게임 승/패
    // 게임 결과 보여주기 (승/패)
    public void ShowGameResult()
    {
        DisableAllCanvas();
        _checkResultCanvas.enabled = true;
    }
    #endregion
}