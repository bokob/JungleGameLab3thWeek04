using UnityEngine;
using Kmk;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class Card : ScriptableObject
{
    public int number; //카드
    public Define.CardShape Shape; //카드문양
    

}
