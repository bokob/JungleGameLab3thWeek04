using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class UI_OpeningCanvas : MonoBehaviour
{
    //VideoClip _openingClip;

    //void Awake()
    //{
    //    _openingClip = GetComponentInChildren<VideoPlayer>().clip;
    //}

    //void Start()
    //{
    //    UIManager.Instance.toggleOpeningAction += ToggleOpening;
    //    UIManager.Instance.disableAction += Disable;
    //    gameObject.SetActive(false);
    //}

    //// 오프닝 토글
    //public void ToggleOpening()
    //{
        
    //    //UIManager.Instance.DisableAllCanvas();
    //    gameObject.SetActive(true);
    //    StartCoroutine(WaitCoroutine((float)_openingClip.length, () =>
    //    {
    //        GameManager.Instance.GamePhase = Define.GamePhase.Start;
    //        UIManager.Instance.disableAction?.Invoke();
    //        UIManager.Instance.toggleGameTitleAction?.Invoke();
    //    }));
    //}

    //public void Disable()
    //{
    //    gameObject.SetActive(false);
    //    Debug.LogWarning("오프닝 비활성화");
    //}

    //IEnumerator WaitCoroutine(float time, Action action)
    //{
    //    yield return new WaitForSeconds(time);
    //    action.Invoke();
    //}
}