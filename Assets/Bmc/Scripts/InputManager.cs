using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Bmc;

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
    #endregion

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Init();
    }

    void Init()
    {
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

        _usedCardInputAction.Enable();
        _ruleInputAction.Enable();
        _upInputAction.Enable();
        _spotInputAction.Enable();
        _downInputAction.Enable();
        _drawInputAction.Enable();
        _stopInputAction.Enable();
        _exitInputAction.Enable();

        _usedCardInputAction.started += OnUsedCard;
        _ruleInputAction.started += OnRule;
        _upInputAction.started += OnUp;
        _spotInputAction.started += OnSpot;
        _downInputAction.started += OnDown;
        _drawInputAction.started += OnDraw;
        _stopInputAction.started += OnStop;
        _exitInputAction.started += OnExit;
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
}