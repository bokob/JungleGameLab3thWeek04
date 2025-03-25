using Bmc;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Define.PlayState CurrentState => _currentState;
    // Revolver _revolver;
    bool _isLive;
    Define.PlayState _currentState;

    public void ChooseDecision()
    {
    }

    public void Play()
    {
        int enemyPoint = CardManager.Instance.CalculatePoint().Item1;
        int playerPoint = CardManager.Instance.CalculatePoint().Item2;

        while (enemyPoint < playerPoint)
        {
            CardManager.Instance.Dealing();
            enemyPoint = CardManager.Instance.CalculatePoint().Item1;
        }

        _currentState = Define.PlayState.Check;
        GameManager.Instance.IsPlayerTurn = true;
        GameManager.Instance.CheckState();
    }
}