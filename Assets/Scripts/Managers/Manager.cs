using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    public static Manager Instance => _instance;

    //#region 매니저
    //UIManager _ui = new UIManager();
    //InputManager _input = new InputManager();
    //SoundManager _sound = new SoundManager();
    //CardManager _card = new CardManager();
    //GameManager _game = new GameManager();

    //public static UIManager UI => Instance._ui;
    //public static InputManager Input => Instance._input;
    //public static SoundManager Sound => Instance._sound;
    //public static CardManager Card => Instance._card;
    //public static GameManager Game => Instance._game;
    //#endregion

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

        DataManager.Instance.Init();
        UIManager.Instance.Init();
        InputManager.Instance.Init();
        SoundManager.Instance.Init();
        CardManager.Instance.Init();
        GameManager.Instance.Init();
    }  
}