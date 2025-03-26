using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Bmc;
using System;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance => _instance;

    #region Canvas
    Canvas _mainCanvas;         // 메인
    Canvas _drawCanvas;         // 드로우
    Canvas _guessCanvas;        // 상대 카드 점수 예측
    Canvas _guessResultCanvas;  // 예측 결과
    Canvas _ruleCanvas;         // 룰
    Canvas _usedCardCanvas;     // 사용한 카드
    Canvas _gameStartCanvas;     // 게임시작
    Canvas _gameClearCanvas;     // 게임 클리어
    Canvas _gameOverCanvas;     // 게임 오버
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

    UI_UsedCardCanvas _uiUsedCardCanvas;
    public UI_UsedCardCanvas UIUsedCardCanvas => _uiUsedCardCanvas;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Init();
    }

    void Start()
    {
        DescribeAction();
    }

    void Update()
    {
    }

    // 모든 UI 컴포넌트 찾고, 등록할 것이 있으면 등록
    public void Init()
    {
        _mainCanvas = FindAnyObjectByType<UI_MainCanvas>().gameObject.GetComponent<Canvas>();
        _guessCanvas = FindAnyObjectByType<UI_CheckCanvas>().gameObject.GetComponent<Canvas>();
        _guessResultCanvas = FindAnyObjectByType<UI_CheckResultCanvas>().gameObject.GetComponent<Canvas>();
        _ruleCanvas = FindAnyObjectByType<UI_RuleCanvas>().gameObject.GetComponent<Canvas>();
        _drawCanvas = FindAnyObjectByType<UI_DrawCanvas>().gameObject.GetComponent<Canvas>();
        
        _gameStartCanvas = FindAnyObjectByType<GameStartCanvas>().gameObject.GetComponent<Canvas>();
        _gameOverCanvas = FindAnyObjectByType<GameOverCanvas>().gameObject.GetComponent<Canvas>();
        _gameClearCanvas = FindAnyObjectByType<GameOverCanvas>().gameObject.GetComponent<Canvas>();

        _uiUsedCardCanvas = FindAnyObjectByType<UI_UsedCardCanvas>();
        _usedCardCanvas = _uiUsedCardCanvas.gameObject.GetComponent<Canvas>();
    }

    public void DescribeAction()
    {
        InputManager.Instance.toggleUsedCardAction += ToggleUsedCard;
        InputManager.Instance.toggleRuleAction += ToggleRule;
        InputManager.Instance.upAction += () => Guess(1);
        InputManager.Instance.spotAction += () => Guess(2);
        InputManager.Instance.downAction += () => Guess(3);
        InputManager.Instance.drawAction += DrawCard;
        InputManager.Instance.stopAction += StopDrawCard;
        InputManager.Instance.exitAction += GameExit;
    }

    // 모든 캔버스 컴포넌트 비활성화
    public void DisableAllCanvas()
    {
        _mainCanvas.enabled = false;
        _guessCanvas.enabled = false;
        _guessResultCanvas.enabled = false;
        _ruleCanvas.enabled = false;
        _drawCanvas.enabled = false;
        _usedCardCanvas.enabled = false;
        _gameStartCanvas.enabled = false;
        _gameOverCanvas.enabled = false;
        _gameClearCanvas.enabled = false;
    }

    public void ToggleMain()
    {
        DisableAllCanvas();
        _mainCanvas.enabled = !_mainCanvas.enabled;
    }

    // 게임 종료 (추후에 게임 매니저로 이동)
    public void GameExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    #region 드로우 창
    // 드로우 창 보여주기
    public void ToggleDraw()
    {
        _drawCanvas.enabled = !_drawCanvas.enabled;
        Debug.Log("드로우 토글");
    }

    // 카드 뽑기
    public void DrawCard()
    {
        CardManager.Instance.Dealing();
    }

    // 카드 뽑기 멈춤
    public void StopDrawCard()
    {
        Debug.Log("카드 뽑기 멈춤");
        ToggleDraw();
        GameManager.Instance.Player.CurrentState = Define.PlayState.Guess;
        GameManager.Instance.Enemy.CurrentState = Define.PlayState.Guess;
        GameManager.Instance.IsPlayerTurn = false;
        GameManager.Instance.CheckState();
        ToggleGuessText();
    }
    #endregion

    #region 사용한 카드
    // 버린 카드 토글 (추후에 마우스 Hover에 의해 나오게 하기)
    public void ToggleUsedCard()
    {
        Debug.Log("사용한 카드 토글");
        _usedCardCanvas.enabled = !_usedCardCanvas.enabled;
    }
    #endregion

    #region 체크
    // 추측 문구 토글(up, spot, down)
    public void ToggleGuessText()
    {
        _guessCanvas.enabled = !_guessCanvas.enabled;
    }

    // 추측 (up: 1, spot: 2, down: 3) (추후에 플레이어에 이동)
    public void Guess(int idx)
    {
        switch (idx)
        {
            case 1:
                Debug.Log("up");
                GameManager.Instance.PlayerGuess = Define.Decision.Up;
                break;
            case 2:
                Debug.Log("spot");
                GameManager.Instance.PlayerGuess = Define.Decision.BlackJack;
                break;
            case 3:
                Debug.Log("down");
                GameManager.Instance.PlayerGuess = Define.Decision.Down;
                break;
        }
        DisableAllCanvas();
        GameManager.Instance.Enemy.CurrentState = Define.PlayState.None;
        GameManager.Instance.Player.CurrentState = Define.PlayState.None;
        ToggleGuessResult();
    }
    #endregion

    #region 체크 결과
    // 플레이어 점수(카드 숫자 총합) 보여주기 => 총 쏘는 연출로 넘어감
    public void ToggleGuessResult()
    {
        DisableAllCanvas();
        Tuple<int, int> points = CardManager.Instance.CalculatePoint();
        _guessResultCanvas.GetComponent<UI_CheckResultCanvas>().SetRoundResult();
        //디시전 표시
        //points.item1 = 적 점수
        //points.item2 = 플레이어 점수
        Debug.Log(points.Item1 + " , " + points.Item2);
        _guessResultCanvas.enabled = true;
    }
    #endregion

    #region 룰
    // 룰 토글
    public void ToggleRule()
    {
        Debug.Log("룰 토글");
        _ruleCanvas.enabled = !_ruleCanvas.enabled;
    }
    #endregion

    #region 게임 승/패
    // 게임 결과 토글 (승/패)
    public void ToggleGameResult()
    {
        DisableAllCanvas();
        _guessResultCanvas.enabled = !_guessResultCanvas.enabled;
    }
    #endregion
}