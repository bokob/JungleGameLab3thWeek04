using UnityEngine;
using System.Collections;
using Bmc;
public class Revolver : MonoBehaviour
{
    [SerializeField] Animator _animator; // 애니메이션 제어
    [SerializeField] GameObject owner; // 리볼버의 주인 찾기
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
        owner = transform.root.gameObject;
        _animator = owner.GetComponent<Animator>();
        _muzzleFlashEffect = GetComponentInChildren<ParticleSystem>();
    }





    public IEnumerator Shoot() // 총알 발사 판단.
    {

        int randomValue = Random.Range(1, 6);
        _animator.SetTrigger("Shoot_Trigger"); // 살자 애니메이션 실행
        yield return new WaitForSeconds(2.0f); // 애니메이션 대기

        if (ammo >= randomValue)
        {
            Debug.Log("Die");
            //GameManager.Instance.Player.CurrentState = Define.PlayState.Death;
            _muzzleFlashEffect.Play(); // + 파티클 효과 넣어서 껏다켯다
            if (owner.gameObject.name == "Player")
            {
                owner.GetComponent<Player>().CurrentState = Define.PlayState.Death;
            }
            else if(owner.gameObject.name == "Enemy")
            {
                owner.GetComponent<Enemy>().CurrentState = Define.PlayState.Death;
            }

            
            // + 빨간색 UI 화면으로 죽는겨 표시!
        }
        else
        {
            Debug.Log("살았다 슈발 ㅠㅠ + 총알 추가");
            ammo += 1;
        }

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
