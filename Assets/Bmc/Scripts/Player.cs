using Bmc;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Define.PlayState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
        }
    }

    Revolver _revolver;
    public Revolver Revolver => _revolver;
    Define.PlayState _currentState;
}