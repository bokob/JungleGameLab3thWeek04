using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;    // 시네마신 카메라가 플레이어의 자식으로 있다고 가정
    [SerializeField] float mouseSensitivity = 5f;   // 마우스 감도
    [SerializeField] float xRotation = 0f;          // x축 회전

    private void Start()
    {
        _playerTransform = transform.parent;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;                                            // 마우스 y축 입력을 위아래 x축 회전량으로 설정
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);                  // 위아래 제한
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // 카메라 위아래 회전

        _playerTransform.Rotate(Vector3.up * mouseX);                   // 플레이어 좌우 회전
    }
}