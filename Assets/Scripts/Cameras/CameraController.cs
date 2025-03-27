using System.Collections;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    #region 플레이어 머리 회전
    [SerializeField] Transform _headBone; // 머리 Bone (애니메이션과 함께 움직이는 Bone)
    [SerializeField] float _mouseSensitivity = 5f;
    [SerializeField] float _xRotation = 0f;
    [SerializeField] float _yRotation = 0f;
    #endregion

    #region 카메라 전환
    [SerializeField] GameObject _gameCamera;    // 플레이어 시점
    [SerializeField] GameObject _tableCamera;   // 탑뷰
    [SerializeField] bool _isTable;             // 현재 뷰가 탑뷰인지 여부
    #endregion

    void Awake()
    {
        Init();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70f, 70f); // 위아래 고개 제한

        _yRotation += mouseX; // 좌우 회전 누적 (제한 없음)
        _yRotation = Mathf.Clamp(_yRotation, -60f, 60f); // 좌우 고개 제한

        /* 추후에 GuessResult 시작 및 종료 때 각각 호출 */
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchCamera();
        }

        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    StartCoroutine(GuessResultDirection(3f));
        //}
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

    void Init()
    {
        _isTable = false;
        _gameCamera = FindAnyObjectByType<Camera_Game>().gameObject;
        _tableCamera = FindAnyObjectByType<Camera_Table>().gameObject;
        _gameCamera.SetActive(true);
        _tableCamera.SetActive(false);
    }

    // 카메라 시점 변환
    public void SwitchCamera()
    {
        Debug.LogWarning("카메라 전환");
        _isTable = !_isTable;
        _gameCamera.SetActive(!_isTable);
        _tableCamera.SetActive(_isTable);
    }

    // 추측 결과 연출
    public IEnumerator GuessResultDirection(float time, bool playerWin, bool enemyWin)
    {
        SwitchCamera();
        yield return new WaitForSeconds(time);
        SwitchCamera();
        yield return new WaitForSeconds(time / 2);
        GameManager.Instance.CallShootCoroutine(playerWin, enemyWin);
    }
}