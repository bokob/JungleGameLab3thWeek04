using UnityEngine;
using System.Collections;

public class Reload_Animation : MonoBehaviour
{
    Animator animator;
    public GameObject Bullet;
    public float bulletSpeed = 5f;  // 총알의 이동 속도

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Reload_Anim());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Reload_Anim()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(0.4f);

        // 총알 이동 시작
        StartCoroutine(MoveBulletForward());

        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("Spin");
    }

    IEnumerator MoveBulletForward()
    {
        Vector3 startPosition = Bullet.transform.position;  // Bullet의 시작 위치
        Vector3 targetPosition = startPosition + Bullet.transform.up * 0.7f;  // 목표 위치 (10 단위만큼 앞에 위치하도록 설정)

        float elapsedTime = 0f;
        float duration = 0.2f;  // 이동하는데 걸리는 시간 (2초 동안 이동)

        while (elapsedTime < duration)
        {
            Bullet.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동 후 목표 위치에 정확히 도달
        Bullet.transform.position = targetPosition;

        Destroy(Bullet);
    }
}
