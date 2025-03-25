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

    public void Test()
    {
        Debug.Log("테스트");
    }
}