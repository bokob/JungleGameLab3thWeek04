using UnityEngine;

/// <summary>
/// 플레이어 정보를 담고 있는 클래스
/// </summary>
public class Player : MonoBehaviour
{
    public Define.PlayState CurrentState { get => _currentState; set => _currentState = value; }
    public Revolver Revolver => _revolver;

    Define.PlayState _currentState;
    Revolver _revolver;



    void Start()
    {
        _revolver = GetComponentInChildren<Revolver>();

        InputManager.Instance.drawAction += DrawCard;
        InputManager.Instance.stopAction += StopDrawCard;
        InputManager.Instance.upAction += () => Guess(1);
        InputManager.Instance.spotAction += () => Guess(2);
        InputManager.Instance.downAction += () => Guess(3);
    }

    // 카드 뽑기
    public void DrawCard()
    {
        CardManager.Instance.DrawCard();
    }

    // 카드 뽑기 멈춤
    public void StopDrawCard()
    {
        Debug.Log("카드 뽑기 멈춤");
        UIManager.Instance.toggleDrawCanvasAction?.Invoke();
        //UIManager.Instance.ToggleDraw();

        GameManager.Instance.Player.CurrentState = Define.PlayState.Guess;
        GameManager.Instance.Enemy.CurrentState = Define.PlayState.Guess;
        GameManager.Instance.IsPlayerTurn = false;
        GameManager.Instance.CheckState();

        UIManager.Instance.toggleGuessTextAction?.Invoke();

        //UIManager.Instance.ToggleGuessText();
    }

    // 추측 (up: 1, spot: 2, down: 3) (추후에 플레이어에 이동)
    public void Guess(int idx)
    {
        switch (idx)
        {
            case 1:
                Debug.Log("up");
                GameManager.Instance.PlayerGuess = Define.Guess.Up;
                break;
            case 2:
                Debug.Log("spot");
                GameManager.Instance.PlayerGuess = Define.Guess.Spot;
                break;
            case 3:
                Debug.Log("down");
                GameManager.Instance.PlayerGuess = Define.Guess.Down;
                break;
        }
        UIManager.Instance.DisableAllCanvas();
        
        GameManager.Instance.Enemy.CurrentState = Define.PlayState.None;
        GameManager.Instance.Player.CurrentState = Define.PlayState.None;

        UIManager.Instance.toggleGuessResultCanvasAction?.Invoke(); // 추측 결과 UI 
    }
}