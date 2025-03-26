using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHoverUI : MonoBehaviour
{
    private TextMeshProUGUI _uiText; // UI에 표시될 텍스트
    public float detectionRadius = 2f; // 감지 반경, 변경예정

    private Transform _player;

    void Start()
    {
        _uiText = GameObject.FindAnyObjectByType<MouseUICanvas>().GetComponent<TextMeshProUGUI>();

        _player = Camera.main.transform; // 카메라 기준으로 거리 측정
        if (_uiText != null)
        {
            _uiText.gameObject.SetActive(false); // 처음에는 비활성화
        }
    }

    void Update()
    {



        //    if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePosition = Input.mousePosition;
        //    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        //    RaycastHit hit;

        //    Debug.Log(1);
        //    MouseUIData data = hit.transform.GetComponent<MouseUIData>();

        //    if (mouseUIDataText != null && _uiText != null)
        //    {
        //        _uiText.text = mouseUIDataText; // MouseUI 스크립트에서 정보 가져오기
        //        _uiText.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        _uiText.gameObject.SetActive(false);
        //    }
        //}
        //else
        //{
        //    if (_uiText != null)
        //        _uiText.gameObject.SetActive(false);
        //}
    }
}
