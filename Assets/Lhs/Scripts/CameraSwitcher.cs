using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] GameObject _playerCamera;
    [SerializeField] GameObject _gameCamera;

    private bool _isSwitching;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        _isSwitching = false;
        

        _playerCamera = FindAnyObjectByType<CameraController>().gameObject;
        _gameCamera = FindAnyObjectByType<Table_Camera>().gameObject;
        _gameCamera.SetActive(false);
    }

    public void SwitchCamera()
    {
        if (!_isSwitching)
        {
            _gameCamera.SetActive(true);

        }
        else
        {
            _gameCamera.SetActive(false);
        }
        _isSwitching = !_isSwitching;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchCamera();
        }

    }
}
