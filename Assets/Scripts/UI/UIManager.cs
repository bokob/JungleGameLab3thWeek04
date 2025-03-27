using TMPro;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
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

    #region Reload Canvas GameObject (영상)
    GameObject _playerReload;
    GameObject _enemyReload;
    #endregion

    #region TextMeshPro
    TextMeshProUGUI _highestWinStreak;
    TextMeshProUGUI _gameStartWinStreak;
    TextMeshProUGUI _gameClearWinStreak;
    #endregion

    Image _crownImage;  // 왕관 이미지

    public UI_UsedCardCanvas UIUsedCardCanvas => _uiUsedCardCanvas;
    UI_UsedCardCanvas _uiUsedCardCanvas;    // 사용한 카드

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
        SubscribeAction();
        ToggleGameStart();
    }

    // 모든 UI 컴포넌트 찾고, 등록할 것이 있으면 등록
    public void Init()
    {
        // UI
        _mainCanvas = FindAnyObjectByType<UI_MainCanvas>().gameObject.GetComponent<Canvas>();
        _drawCanvas = FindAnyObjectByType<UI_DrawCanvas>().gameObject.GetComponent<Canvas>();
        _guessCanvas = FindAnyObjectByType<UI_GuessCanvas>().gameObject.GetComponent<Canvas>();
        _guessResultCanvas = FindAnyObjectByType<UI_GuessResultCanvas>().gameObject.GetComponent<Canvas>();

        _uiUsedCardCanvas = FindAnyObjectByType<UI_UsedCardCanvas>();
        _usedCardCanvas = _uiUsedCardCanvas.gameObject.GetComponent<Canvas>();

        _ruleCanvas = FindAnyObjectByType<UI_RuleCanvas>().gameObject.GetComponent<Canvas>();

        // 장전 영상
        _playerReload = FindAnyObjectByType<UI_PlayerReload>().gameObject;
        _enemyReload = FindAnyObjectByType<UI_EnemyReload>().gameObject;
        _playerReload.SetActive(false);
        _enemyReload.SetActive(false);

        // 게임 흐름
        _gameStartCanvas = FindAnyObjectByType<UI_GameStartCanvas>().gameObject.GetComponent<Canvas>();
        _gameOverCanvas = FindAnyObjectByType<UI_GameOverCanvas>().gameObject.GetComponent<Canvas>();
        _gameClearCanvas = FindAnyObjectByType<UI_GameClearCanvas>().gameObject.GetComponent<Canvas>();

        _highestWinStreak = FindAnyObjectByType<UI_HighestWinStreak>().gameObject.GetComponent<TextMeshProUGUI>();
        _gameStartWinStreak = FindAnyObjectByType<UI_GameStartWinStreak>().gameObject.GetComponent<TextMeshProUGUI>();
        _gameClearWinStreak = FindAnyObjectByType<UI_GameClearWinstreak>().gameObject.GetComponent<TextMeshProUGUI>();

        _crownImage = FindAnyObjectByType<UI_CrownImage>().gameObject.GetComponent<Image>();
    }

    // 액션 등록
    public void SubscribeAction()
    {
        InputManager.Instance.toggleUsedCardAction += ToggleUsedCard;
        InputManager.Instance.toggleRuleAction += ToggleRule;
    }

    // 게임 시작 UI
    public void ToggleGameStart()
    {
        _gameStartCanvas.enabled = true;

        int highestWinstreak = PlayerPrefs.GetInt("HighestWinstreak");
        int winStreak = PlayerPrefs.GetInt("Winstreak");

        if (highestWinstreak >= 20)
            _crownImage.enabled = true;

        _highestWinStreak.text = $"Highest Winstreak: {highestWinstreak}";
        _gameStartWinStreak.text = $"Winstreak: {winStreak}";
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

    // 메인 캔버스 토글
    public void ToggleMain()
    {
        DisableAllCanvas();
        _mainCanvas.enabled = !_mainCanvas.enabled;
    }

    #region 드로우 창
    // 드로우 창 보여주기
    public void ToggleDraw()
    {
        _drawCanvas.enabled = !_drawCanvas.enabled;
        Debug.Log("드로우 토글");
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


    #endregion

    #region 체크 결과 확인
    // 플레이어 점수(카드 숫자 총합) 보여주기 => 총 쏘는 연출로 넘어감
    public void ToggleGuessResult()
    {
        DisableAllCanvas();
        Tuple<int, int> points = CardManager.Instance.CalculatePoint();
        _guessResultCanvas.GetComponent<UI_GuessResultCanvas>().SetRoundResult();
        _guessResultCanvas.enabled = !_guessResultCanvas.enabled;
        //디시전 표시
        //points.item1 = 적 점수
        //points.item2 = 플레이어 점수
        Debug.Log(points.Item1 + " , " + points.Item2);
    }
    #endregion

    #region 규칙
    // 룰 토글
    public void ToggleRule()
    {
        Debug.Log("룰 토글");
        _ruleCanvas.enabled = !_ruleCanvas.enabled;
    }
    #endregion

    #region 게임 클리어/게임오버
    public void ToggleGameClear()
    {
        _gameClearWinStreak.text = $"Winstreak: {GameManager.Instance.WinStreak}";
        if(GameManager.Instance.HighestWinStreak < GameManager.Instance.WinStreak)
        {
            PlayerPrefs.SetInt("HighestWinstreak", GameManager.Instance.WinStreak);
        }
        _gameClearCanvas.enabled = !_gameClearCanvas.enabled;
    }

    public void ToggleGameOver()
    {
        _gameOverCanvas.enabled = !_gameOverCanvas.enabled;
    }
    #endregion

    #region 리로드 영상 토글
    public IEnumerator PlayPlayerReloadCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        _playerReload.SetActive(true);
        yield return new WaitForSeconds(2f);
        _playerReload.SetActive(false);
    }

    public IEnumerator PlayEnemyReloadCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        _enemyReload.SetActive(true);
        yield return new WaitForSeconds(2f);
        _enemyReload.SetActive(false);
    }
    #endregion
}