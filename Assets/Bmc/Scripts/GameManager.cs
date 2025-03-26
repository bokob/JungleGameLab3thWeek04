using Bmc;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    static GameManager _instance;

    public bool IsPlayerTurn { get; set; }
    bool _isPlayerTurn = false;
    Player _player;
    Enemy _enemy;

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

    }

    public void Init()
    {
        Debug.Log("게임 매니저 생성");
    }

    // 플레이어 및 적 상태에 따른 상황 판단
    public void CheckState()
    {
        if(_isPlayerTurn)
        {
            if (_player.CurrentState == Define.PlayState.Draw)
            {
                UIManager.Instance.ShowDraw();
            }
        }
        else
        {
            if(_enemy.CurrentState == Define.PlayState.Draw)
            {
                _enemy.Play();  // 카드 뽑기
            }
            else if(_enemy.CurrentState == Define.PlayState.Check)
            {
                _enemy.ChooseDecision();    // Check일 때 뭐 할지 결정
            }
        }
    }
}