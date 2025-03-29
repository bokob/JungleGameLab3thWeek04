using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;
    public static InputManager Instance => _instance;

    InputSystem_Actions _inputActions;

    #region InputAction
    InputAction _usedCardInputAction;
    InputAction _ruleInputAction;
    InputAction _upInputAction;
    InputAction _spotInputAction;
    InputAction _downInputAction;
    InputAction _drawInputAction;
    InputAction _stopInputAction;
    InputAction _exitInputAction;
    InputAction _startGameInputAction;
    InputAction _CylinderCheckInputAction;
    #endregion

    #region 액션
    public Action toggleUsedCardAction;
    public Action toggleRuleAction;
    public Action upAction;
    public Action spotAction;
    public Action downAction;
    public Action drawAction;
    public Action stopAction;
    public Action exitAction;
    public Action startGameAction;
    public Action cylinderCheckAction;

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
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();

        _usedCardInputAction = _inputActions.Player.UsedCard;
        _ruleInputAction = _inputActions.Player.Rule;
        _upInputAction = _inputActions.Player.Up;
        _spotInputAction = _inputActions.Player.Spot;
        _downInputAction = _inputActions.Player.Down;
        _drawInputAction = _inputActions.Player.Draw;
        _stopInputAction = _inputActions.Player.Stop;
        _exitInputAction = _inputActions.Player.Exit;
        _startGameInputAction = _inputActions.Player.StartGame;
        _CylinderCheckInputAction = _inputActions.Player.CylinderCheck;

        _usedCardInputAction.Enable();
        _ruleInputAction.Enable();
        _upInputAction.Enable();
        _spotInputAction.Enable();
        _downInputAction.Enable();
        _drawInputAction.Enable();
        _stopInputAction.Enable();
        _exitInputAction.Enable();
        _startGameInputAction.Enable();
        _CylinderCheckInputAction.Enable();

        _usedCardInputAction.started += OnUsedCard;
        _ruleInputAction.started += OnRule;
        _upInputAction.started += OnUp;
        _spotInputAction.started += OnSpot;
        _downInputAction.started += OnDown;
        _drawInputAction.started += OnDraw;
        _stopInputAction.started += OnStop;
        _exitInputAction.started += OnExit;
        _startGameInputAction.started += OnStartGame;
        _CylinderCheckInputAction.started += OnCylinderCheck;
    }

    void OnUsedCard(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            toggleUsedCardAction?.Invoke();
        }
    }

    void OnRule(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            toggleRuleAction?.Invoke();
        }
    }

    void OnUp(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.CurrentState == Define.PlayState.Guess)
        {
            if (context.phase == InputActionPhase.Started)
            {
                upAction?.Invoke();
            }
        }
    }

    void OnSpot(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.CurrentState == Define.PlayState.Guess)
        {
            if (context.phase == InputActionPhase.Started)
            {
                spotAction?.Invoke();
            }
        }
    }

    void OnDown(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.CurrentState == Define.PlayState.Guess)
        {
            if (context.phase == InputActionPhase.Started)
            {
                downAction?.Invoke();
            }
        }
    }

    void OnDraw(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.CurrentState == Define.PlayState.Draw)
        {
            if (context.phase == InputActionPhase.Started)
            {
                drawAction?.Invoke();
            }
        }
    }

    void OnStop(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.CurrentState == Define.PlayState.Draw)
        {
            if (context.phase == InputActionPhase.Started)
            {
                stopAction?.Invoke();
            }
        }
    }

    void OnExit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            exitAction?.Invoke();
        }
    }

    void OnStartGame(InputAction.CallbackContext context)
    {
        startGameAction?.Invoke();
    }

    void OnCylinderCheck(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.Player.CurrentState == Define.PlayState.Guess || GameManager.Instance.Player.CurrentState == Define.PlayState.Draw)
        {
            cylinderCheckAction?.Invoke();
        }
        
        
    }

    public void Clear()
    {
        // UI Action 해제
        toggleUsedCardAction = null;
        toggleRuleAction = null;
        upAction = null;
        spotAction = null;
        downAction = null;
        drawAction = null;
        stopAction = null;
        exitAction = null;
        startGameAction = null;
        cylinderCheckAction = null;

        // Input Action 비활성화
        _usedCardInputAction.Disable();
        _ruleInputAction.Disable();
        _upInputAction.Disable();
        _spotInputAction.Disable();
        _downInputAction.Disable();
        _drawInputAction.Disable();
        _stopInputAction.Disable();
        _exitInputAction.Disable();
        _startGameInputAction.Disable();
        _CylinderCheckInputAction.Disable();

        // Input Action 해제
        _usedCardInputAction = null;
        _ruleInputAction = null;
        _upInputAction = null;
        _spotInputAction = null;
        _downInputAction = null;
        _drawInputAction = null;
        _stopInputAction = null;
        _exitInputAction = null;
        _startGameInputAction = null;
        _CylinderCheckInputAction = null;

        // Input System 비활성화
        _inputActions.Disable();
    }
}