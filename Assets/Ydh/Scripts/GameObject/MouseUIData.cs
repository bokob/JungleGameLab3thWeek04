using UnityEngine;

public class MouseUIData : MonoBehaviour
{
    private string _mouseUIText;

    public string MouseUIText
    {
        get { return _mouseUIText; }
        set { _mouseUIText = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mouseUIText = "TextNull";
    }

    // Update is called once per frame
    void Update()
    {
        //총탄의 경우 총알 수를 Text 함수
        //카드의 경우 카드의 총 합을 Text 함수
    }
}
