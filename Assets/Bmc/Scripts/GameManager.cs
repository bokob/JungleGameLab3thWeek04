using Bmc;
using System;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public Player Player => _player;
    public Enemy Enemy => _enemy;

    static GameManager _instance;
    public bool IsPlayerTurn { get; set; }
    Player _player;
    Enemy _enemy;
    public Define.Decision EnemyDecision { get; set; }
    public Define.Decision PlayerDecision { get; set; }

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            Invoke(nameof(NewRound), 1f);
        }
    }

    public void Init()
    {
        Debug.Log("게임 매니저 생성");
        IsPlayerTurn = false;
        _player = FindAnyObjectByType<Player>(); // 테스트 코드
        _enemy = FindAnyObjectByType<Enemy>(); // 테스트 코드
        _player.CurrentState = Define.PlayState.None;
        _enemy.CurrentState = Define.PlayState.None;
    }

    public void NewRound()
    {
        _enemy.CurrentState = Define.PlayState.None;
        _player.CurrentState = Define.PlayState.None;
        IsPlayerTurn = false;
        CardManager.Instance.FirstDealing();
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
                StartCoroutine(_enemy.Play());  // 카드 뽑기
            }
            else if (_enemy.CurrentState == Define.PlayState.Guess)
            {
                Enemy.ChooseDecision();
            }
        }
    }
}