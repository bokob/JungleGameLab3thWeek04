using UnityEngine;
using System.Collections;
public class Revolver : MonoBehaviour
{
    public GameObject Owner => _owner;
    [SerializeField] Animator _animator;    // 애니메이션 제어
    [SerializeField] GameObject _owner;     // 리볼버의 주인 찾기
    [SerializeField] GameObject _camera;    // 캐릭터 카메라 찾기
    ParticleSystem _muzzleFlashEffect;      // Muzzle Flash 파티클 (자식 오브젝트에서 찾음)
    int _ammo; // 총알 개수 제어

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _ammo = 1;
        _owner = transform.root.gameObject;
        _animator = _owner.GetComponent<Animator>();
        _muzzleFlashEffect = GetComponentInChildren<ParticleSystem>();
        _camera = FindAnyObjectByType<CameraController>().gameObject;
    }


    public IEnumerator Shoot() // 총알 발사(자기 머리 쏘기) 판단
    {
        int randomValue = Random.Range(1, 6);

        // 총 쏘는 애니메이션 실행 및 대기
        _animator.SetTrigger("Shoot_Trigger");
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
                _animator.SetTrigger("Die");
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
                _animator.SetTrigger("Die");
                StartCoroutine(Raggdoll_Active());
                yield return new WaitForSeconds(1f);

                // 게임 오버
                GameManager.Instance.GamePhase = Define.GamePhase.End;
                if (GameManager.Instance.Player.CurrentState != Define.PlayState.Death)
                {
                    int currentWinstreak = PlayerPrefs.GetInt("Winstreak");
                    PlayerPrefs.SetInt("Winstreak", currentWinstreak + 1);

                    UIManager.Instance.DisableAllCanvas();
                    UIManager.Instance.ToggleGameClear();
                }
            }
        }
        else
        {
            Debug.Log("안죽음");
            _ammo += 1;
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

    // 사망 시, Ragdoll 활성화
    IEnumerator Raggdoll_Active()
    {
        yield return new WaitForSeconds(0.2f);
        _animator.enabled = false;
    }
}