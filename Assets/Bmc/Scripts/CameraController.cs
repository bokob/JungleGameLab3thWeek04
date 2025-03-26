using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _headBone; // 머리 Bone (애니메이션과 함께 움직이는 Bone)
    [SerializeField] float _mouseSensitivity = 5f;
    [SerializeField] float _xRotation = 0f;
    [SerializeField] float _yRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70f, 70f); // 위아래 고개 제한

        _yRotation += mouseX; // 좌우 회전 누적 (제한 없음)
        _yRotation = Mathf.Clamp(_yRotation, -60f, 60f); // 좌우 고개 제한
    }

    void LateUpdate()
    {
        if (_headBone != null)
        {
            // 애니메이션의 기본 회전에 추가 회전 적용
            Quaternion headRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            _headBone.localRotation = headRotation;
        }
    }
}
/*public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;    // 시네마신 카메라가 플레이어의 자식으로 있다고 가정
    [SerializeField] float _mouseSensitivity = 5f;   // 마우스 감도
    [SerializeField] float _xRotation = 0f;          // x축 회전

    private void Start()
    {
        _playerTransform = transform.parent;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _xRotation -= mouseY;                                            // 마우스 y축 입력을 위아래 x축 회전량으로 설정
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);                  // 위아래 제한
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);  // 카메라 위아래 회전

        _playerTransform.Rotate(Vector3.up * mouseX);                   // 플레이어 좌우 회전
    }
}*/