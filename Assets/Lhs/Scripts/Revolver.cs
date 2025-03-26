using UnityEngine;
using System.Collections;
using Bmc;
using NUnit.Framework.Constraints;
public class Revolver : MonoBehaviour
{
    [SerializeField] Animator _animator; // 애니메이션 제어
    [SerializeField] GameObject _owner; // 리볼버의 주인 찾기
    public GameObject Owner => _owner;
    [SerializeField] GameObject _Camera; // 캐릭터 카메라 찾기
    private ParticleSystem _muzzleFlashEffect; // Muzzle Flash 파티클 (자식 오브젝트에서 찾음)

    public int ammo; // 총알 개수 제어



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        ammo = 1;
        _owner = transform.root.gameObject;
        _animator = _owner.GetComponent<Animator>();
        _muzzleFlashEffect = GetComponentInChildren<ParticleSystem>();
        _Camera = FindAnyObjectByType<CameraController>().gameObject;
    }


    public IEnumerator Shoot() // 총알 발사 판단.
    {

        int randomValue = Random.Range(1, 6);
        _animator.SetTrigger("Shoot_Trigger"); // 총 쏘는 애니메이션 실행
        yield return new WaitForSeconds(2.0f); // 애니메이션 대기

        if (ammo >= randomValue)
        {
            Debug.Log("Die");
            //GameManager.Instance.Player.CurrentState = Define.PlayState.Death;
            _muzzleFlashEffect.Play(); // + 파티클 효과 넣어서 껏다켯다
            if (_owner.gameObject.name == "Player")
            {
                Debug.Log("플레이어 사망");
                _owner.GetComponent<Player>().CurrentState = Define.PlayState.Death;
                _animator.SetTrigger("Die"); // 살자 애니메이션 실행
                StartCoroutine(Raggdoll_Active());
                _Camera.GetComponent<CameraController>().enabled = false;
                yield return new WaitForSeconds(1f);
                GameManager.Instance.GamePhase = Define.GamePhase.End;
                UIManager.Instance.ToggleGameOver();

            }
            else if (_owner.gameObject.name == "Enemy")
            {
                Debug.Log("에너미 사망");
                _owner.GetComponent<Enemy>().CurrentState = Define.PlayState.Death;
                _animator.SetTrigger("Die"); // 살자 애니메이션 실행
                StartCoroutine(Raggdoll_Active());
                yield return new WaitForSeconds(1f);
                GameManager.Instance.GamePhase = Define.GamePhase.End;
                if (GameManager.Instance.Player.CurrentState != Define.PlayState.Death)
                {
                    UIManager.Instance.ToggleGameClear();
                }
            }


            // + 빨간색 UI 화면으로 죽는겨 표시!
        }
        else
        {
            Debug.Log("살았다 슈발 ㅠㅠ + 총알 추가");
            ammo += 1;
            if (_owner.gameObject.GetComponent<Player>() != null)
            {
               
                yield return new WaitForSeconds(2f);
                GameManager.Instance.NewRound();
            }
        }

    }

    IEnumerator Raggdoll_Active()
    {
        yield return new WaitForSeconds(0.2f);
        _animator.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot()); // 리볼버 땡기기 로직 호출(임시)
        }
    }
}
