using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    public static Manager Instance => _instance;

    void Awake()
    {
        _instance = this;
        
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        DataManager.Instance.Init();
        UIManager.Instance.Init();
        InputManager.Instance.Init();
        SoundManager.Instance.Init();
        CardManager.Instance.Init();
        GameManager.Instance.Init();
    }  
}