using Bmc;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Define.PlayState CurrentState => _currentState;
    // Revolver _revolver;
    bool _isLive;
    Define.PlayState _currentState;

    // Check일 때 뭐 할지 결정
    public void ChooseDecision()
    {
    }

    // 카드 뽑기
    public void Play()
    {
    }
}