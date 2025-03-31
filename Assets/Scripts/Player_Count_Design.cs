using System.Collections.Generic;
using UnityEngine;

public class Player_Count_Design : MonoBehaviour
{
    [SerializeField] private GameObject _others; // 다른 사람 프리팹
    [SerializeField] private GameObject _player; // 플레이어 표시 프리팹
    [SerializeField] private GameObject _enemy; // 상대 표시 프리팹

    void Start()
    {
        _others = Resources.Load<GameObject>("Live_Count/Others");
        _player = Resources.Load<GameObject>("Live_Count/Player");
        _enemy = Resources.Load<GameObject>("Live_Count/Enemy");

        

        int childCount = transform.childCount;


        int _Player_Pos = Random.Range(0, childCount);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
