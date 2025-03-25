using Bmc;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Define.PlayState CurrentState => _currentState;
    // Revolver _revolver;
    bool _isLive;
    Define.PlayState _currentState;
}