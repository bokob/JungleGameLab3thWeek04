using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static Unity.VisualScripting.Metadata;
using System.Collections.Generic;
public class Revolver : MonoBehaviour
{
    Animator _revolverAnimator;
    public GameObject Owner => _owner;
    [SerializeField] Animator _playerAnimator;    // 애니메이션 제어
    [SerializeField] GameObject _owner;     // 리볼버의 주인 찾기
    [SerializeField] GameObject _camera;    // 캐릭터 카메라 찾기


    ParticleSystem _muzzleFlashEffect;      // Muzzle Flash 파티클 (자식 오브젝트에서 찾음)
    int _ammo; // 총알 개수 제어

    List<int> _validNumbers = new List<int>();

    private const int _pairCount = 12; // 총알 쌍 개수 6쌍~ 6쌍쌍바 ㅋㅋ
    public GameObject[] PairBullets; // 쌍쌍바 관리할 곳.

    private Transform _cylinderBone; // 실린더 뼈 어딨니
    private float[] _allowedAngles = { 45f, 90f, 135f, 180f, 225f, 270f, 315f, 360f }; // 실린더 각도
    private bool _changeRotation = false;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        //자식 오브젝트 6개 가져올 곳.
        PairBullets = new GameObject[_pairCount];
        for (int i = 0; i < _pairCount; i++)
        {
            PairBullets[i] = transform.GetChild(i).gameObject;
        }

        _validNumbers.AddRange(new int[] { 1, 2, 3, 4, 5, 6 });

        _cylinderBone = FindAnyObjectByType<Revolver_Bone>().transform;

        InputManager.Instance.cylinderCheckAction += CylinderCheck;
        _revolverAnimator = GetComponent<Animator>();
        _ammo = 1;
        _owner = transform.root.gameObject;
        _playerAnimator = _owner.GetComponent<Animator>();
        _muzzleFlashEffect = GetComponentInChildren<ParticleSystem>();

        //_cylinderBone = GetComponentInChildren<>

        _camera = FindAnyObjectByType<CameraController>().gameObject;

        Bullt_Appear();
    }

    public IEnumerator Shoot() // 총알 발사(자기 머리 쏘기) 판단
    {
        int randomValue = Random.Range(1, 6);

        // 총 쏘는 애니메이션 실행 및 대기
        _playerAnimator.SetTrigger("Shoot_Trigger");
        yield return new WaitForSeconds(2.0f); 

        if (_ammo >= randomValue)
        {
            Debug.Log("Die");
            _muzzleFlashEffect.Play(); // 파티클 효과 재생
            SoundManager.Instance.PlayEffect("Shoot");

            if (_owner.gameObject.name == "Player") // 플레이어가 사망
            {
                PlayerPrefs.SetInt("Winstreak", 0);

                Debug.Log("플레이어 사망");
                _owner.GetComponent<Player>().CurrentState = Define.PlayState.Death;

                // 사망 연출
                _playerAnimator.SetTrigger("Die");
                StartCoroutine(Raggdoll_Active());
                _camera.GetComponent<CameraController>().enabled = false;
                yield return new WaitForSeconds(1f);

                // 게임 오버
                GameManager.Instance.GamePhase = Define.GamePhase.End;
                UIManager.Instance.DisableAllCanvas();
                UIManager.Instance.ToggleGameOver();
            }
            else if (_owner.gameObject.name == "Enemy")
            {
                Debug.Log("에너미 사망");
                _owner.GetComponent<Enemy>().CurrentState = Define.PlayState.Death;

                // 사망 연출
                _playerAnimator.SetTrigger("Die");
                StartCoroutine(Raggdoll_Active());
                yield return new WaitForSeconds(1f);

                // 게임 오버
                GameManager.Instance.GamePhase = Define.GamePhase.End;
                if (GameManager.Instance.Player.CurrentState != Define.PlayState.Death)
                {
                    int currentWinstreak = PlayerPrefs.GetInt("Winstreak");
                    GameManager.Instance.WinStreak = currentWinstreak + 1;
                    PlayerPrefs.SetInt("Winstreak", GameManager.Instance.WinStreak);

                    UIManager.Instance.DisableAllCanvas();
                    UIManager.Instance.ToggleGameClear();
                }
            }
        }
        else
        {
            
            Debug.Log("안죽음");
            _ammo += 1;
            Bullt_Appear();
            SoundManager.Instance.PlayEffect("FailShoot");
            UIManager.Instance.DisableAllCanvas();

            // 둘 다 생존
            if (GameManager.Instance.Player.CurrentState != Define.PlayState.Death && GameManager.Instance.Enemy.CurrentState != Define.PlayState.Death)
            {
                // 장전 연출
                if (_owner.name == "Player")
                {
                    SoundManager.Instance.PlayEffect("AfterShootLive2");
                    StartCoroutine(UIManager.Instance.PlayPlayerReloadCoroutine());
                }
                else
                {
                    StartCoroutine(UIManager.Instance.PlayEnemyReloadCoroutine());
                }
            }
        }
    }

    void Bullt_Appear()
    {
        int randomValue = Random.Range(1, _validNumbers.Count); // 인덱스 번호
        int Active_Bullet = _validNumbers[randomValue]; // 활성화 시킬 총알 번호


        PairBullets[Active_Bullet -1].gameObject.SetActive(true);
        PairBullets[Active_Bullet + 5].gameObject.SetActive(true);

        _validNumbers.RemoveAt(randomValue);
    }


        void CylinderCheck()
    {
        if(_owner.name == "Player")
        {
            _revolverAnimator.SetTrigger("Cylinder_Open");
            _playerAnimator.SetTrigger("Sylinder_Check");
        }

    }

    public void Cylinder_Open_Sounds()
    {
        if (_owner.name == "Player")
        {
            _changeRotation = true;
            SoundManager.Instance.PlayEffect("Cylinder_Open");
        }
    }
    public void Cylinder_Close_Sounds()
    {
        if (_owner.name == "Player")
        {
            SoundManager.Instance.PlayEffect("Cylinder_Colse");
        }

    }

    public void Cylinder_Spin_Sounds()
    {
        if (_owner.name == "Player")
        {
            SoundManager.Instance.PlayEffect("Cylinder_Spin");
        }
    }

    public void Cylinder_Rotation()
    {
        if (_owner.name == "Player")
        {
            int randomValue = Random.Range(0, 8);

            _cylinderBone.transform.rotation = Quaternion.Euler(90, 0, 0);


            Debug.Log("돌림");
        }
    }

    // 사망 시, Ragdoll 활성화
    IEnumerator Raggdoll_Active()
    {
        yield return new WaitForSeconds(0.2f);
        _playerAnimator.enabled = false;
    }
}