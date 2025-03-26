using Bmc;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Define.PlayState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
        }
    }
    // Revolver _revolver;
    bool _isLive;
    Define.PlayState _currentState;

    // Check일 때 뭐 할지 결정
    public void ChooseDecision()
    {
    }

    // 카드 뽑기
    public IEnumerator Play()
    {
        int enemyPoint = CardManager.Instance.CalculatePoint().Item1;
        int playerPoint = CardManager.Instance.CalculatePoint().Item2;
        Debug.Log("here");
        while (enemyPoint < playerPoint)
        {
            CardManager.Instance.Dealing();
            yield return new WaitForSeconds(0.5f);
            enemyPoint = CardManager.Instance.CalculatePoint().Item1;
        }

        _currentState = Define.PlayState.None;
        GameManager.Instance.Player.CurrentState = Define.PlayState.Draw;
        GameManager.Instance.IsPlayerTurn = true;
        GameManager.Instance.CheckState();
    }
}