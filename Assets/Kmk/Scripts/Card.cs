using UnityEngine;
using Kmk;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class Card : ScriptableObject
{
    public int _number; //카드
    public Define.CardShape Shape;
    

}
