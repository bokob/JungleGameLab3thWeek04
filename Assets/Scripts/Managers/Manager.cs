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
        /*
         1. UIManager
         2. InputManager
         3. SoundManager
         4. CardManager
         5. GameManager
         */

        UIManager.Instance.Init();
        InputManager.Instance.Init();
        SoundManager.Instance.Init();
        CardManager.Instance.Init();
        GameManager.Instance.Init();
    }  
}