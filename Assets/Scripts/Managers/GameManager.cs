using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    static GameManager _instance;

    #region 게임
    public Define.GamePhase GamePhase { get; set; } // 게임 흐름
    public bool IsPlayerTurn { get; set; }          // 플레이어 차례 여부
    public int IsSeenOpening { get; set; }         // 오프닝 시청 여부
    public int HighestWinStreak { get; set; }       // 최고 연승
    public int WinStreak { get; set; }              // 연승
    #endregion

    #region 카메라
    public CameraController CameraController => _cameraController;
    CameraController _cameraController;
    #endregion

    #region 플레이어
    public Define.Guess PlayerGuess { get; set; }
    public Player Player => _player;
    Player _player;
    #endregion

    #region 적
    public Define.Guess EnemyGuess { get; set; }
    public Enemy Enemy => _enemy;
    Enemy _enemy;
    #endregion

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Init()
    {
        InputManager.Instance.startGameAction += StartGame;
        InputManager.Instance.exitAction += GameExit;

        _player = FindAnyObjectByType<Player>();
        _enemy = FindAnyObjectByType<Enemy>();
        _cameraController = FindAnyObjectByType<CameraController>();

        IsPlayerTurn = false;
        _player.CurrentState = Define.PlayState.None;
        _enemy.CurrentState = Define.PlayState.None;

        LoadGameInfo();
        
        if(IsSeenOpening == 0)
        {
            GamePhase = Define.GamePhase.Opening;
            PlayerPrefs.SetInt("IsSeenOpening", 1);
            UIManager.Instance.ToggleOpening();
        }
        else
        {
            GamePhase = Define.GamePhase.Start;
        }

    }

    // 게임 정보 불러오기
    void LoadGameInfo()
    {
        // 연승
        if (PlayerPrefs.HasKey("Winstreak"))
            WinStreak = PlayerPrefs.GetInt("Winstreak");
        else
            PlayerPrefs.SetInt("Winstreak", 0);

        // 최대 연승
        if (PlayerPrefs.HasKey("HighestWinstreak"))
            WinStreak = PlayerPrefs.GetInt("HighestWinstreak");
        else
            PlayerPrefs.SetInt("HighestWinstreak", 0);

        // 오프닝 시청 기록
        if (PlayerPrefs.HasKey("IsSeenOpening"))
            IsSeenOpening = PlayerPrefs.GetInt("IsSeenOpening");
        else
            PlayerPrefs.SetInt("IsSeenOpening", 0);
    }

    public void NewRound()
    {
        if (_enemy.CurrentState != Define.PlayState.Death && _player.CurrentState != Define.PlayState.Death)
        {
            _enemy.CurrentState = Define.PlayState.None;
            _player.CurrentState = Define.PlayState.None;
            UIManager.Instance.ToggleMain();
            IsPlayerTurn = false;
            CardManager.Instance.StartTurn();
            Debug.Log("성공함");
        }
        else
        {
            Debug.Log("실패함");
        }
    }

    // 플레이어 및 적 상태에 따른 상황 판단
    public void CheckState()
    {
        if (IsPlayerTurn)
        {
            if (_player.CurrentState == Define.PlayState.Draw)
            {
                UIManager.Instance.ToggleDraw();
            }
        }
        else
        {
            if (_enemy.CurrentState == Define.PlayState.Draw)
            {
                StartCoroutine(_enemy.DrawCoroutine());  // 카드 뽑기
            }
            else if (_enemy.CurrentState == Define.PlayState.Guess)
            {
                Enemy.Guess();
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("startGame");
        switch (GamePhase)
        {
            case Define.GamePhase.Opening:
                break;
            case Define.GamePhase.Start:
                Time.timeScale = 1f;
                _cameraController.enabled = true;
                //Cursor.lockState = CursorLockMode.None;
                UIManager.Instance.DisableAllCanvas();
                UIManager.Instance.ToggleMain();
                SoundManager.Instance.PlayBGM();
                GamePhase = Define.GamePhase.Play;
                Invoke("NewRound", 1f);
                break;
            case Define.GamePhase.Play:
                break;
            case Define.GamePhase.End:
                Scene gameScene = SceneManager.GetActiveScene();
                InputManager.Instance.Clear();
                SceneManager.LoadScene(gameScene.name);
                break;
        }
    }

    // 플레이어 및 적에게 총 쏘라고 명령
    public void CallShootCoroutine(bool playerWin, bool enemyWin)
    {
        if (GameManager.Instance.Player.Revolver.IsOpenCylinder)
        {
            GameManager.Instance.Player.Revolver.CylinderCheck();
        }

        Debug.Log("뉴라운드 호출");

        if(playerWin && enemyWin)
            StartCoroutine(Call(3f));
        else
            StartCoroutine(Call(6f));

        if (!playerWin)
        {
            StartCoroutine(_player.Revolver.Shoot());
            Debug.Log("플레이어 슛 호출");
        }

        if (!enemyWin)
        {
            StartCoroutine(_enemy.Revolver.Shoot());
            Debug.Log("에너미 슛 호출");
        }
    }

    IEnumerator Call(float waitTime)
    {
        UIManager.Instance.DisableAllCanvas();
        yield return new WaitForSeconds(waitTime);
        Debug.Log("뉴라운드 코루틴 호출");
        NewRound();
    }

    // 게임 종료 (추후에 게임 매니저로 이동)
    public void GameExit()
    {
        Debug.Log("게임 종료");
        InputManager.Instance.Clear();
        Application.Quit();
    }
}