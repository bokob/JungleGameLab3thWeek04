using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance => _instance;

    #region 영상
    GameObject _opening;
    GameObject _playerReload;
    GameObject _enemyReload;
    VideoClip _openingClip;
    #endregion

    public Action disableAction;                    // 비활성화
    public Action toggleOpeningAction;              // 오프닝 토글
    public Action toggleMainCanvasAction;           // 메인 토글
    public Action toggleGameTitleAction;            // 게임 시작 토글
    public Action toggleGuessTextAction;            // 추측 텍스트 토글
    public Action toggleDrawCanvasAction;           // 드로우 토글 (카드 뽑을지 여부)
    public Action toggleGuessResultCanvasAction;    // 추측 결과 토글
    public Action toggleGameOverCanvasAction;       // 게임 오버 토글
    public Action toggleGameClearCanvasAction;      // 게임 클리어 토글
    public Action updateUsedCardUIAction;           // Used Card UI 업데이트

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // 모든 UI 컴포넌트 찾고, 등록할 것이 있으면 등록
    public void Init()
    {
        // 영상
        _opening = FindAnyObjectByType<UI_OpeningCanvas>().gameObject;
        _openingClip = _opening.GetComponentInChildren<VideoPlayer>().clip;

        _playerReload = FindAnyObjectByType<UI_PlayerReload>().gameObject;
        _enemyReload = FindAnyObjectByType<UI_EnemyReload>().gameObject;
        
        
        _opening.SetActive(false);
        
        
        _playerReload.SetActive(false);
        _enemyReload.SetActive(false);

        toggleGameTitleAction.Invoke();
    }

    // 모든 캔버스 컴포넌트 비활성화
    public void DisableAllCanvas()
    {
        disableAction?.Invoke();
        _opening.SetActive(false);
    }

    // 오프닝 토글
    public void ToggleOpening()
    {
        DisableAllCanvas();
        _opening.SetActive(true);
        StartCoroutine(WaitCoroutine((float)_openingClip.length, () =>
        {
            GameManager.Instance.GamePhase = Define.GamePhase.Start;
            DisableAllCanvas();
            toggleGameTitleAction?.Invoke();
        }));
    }

    #region 리로드 영상 토글
    public IEnumerator PlayPlayerReloadCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        _playerReload.SetActive(true);
        yield return new WaitForSeconds(2f);
        _playerReload.SetActive(false);
    }

    public IEnumerator PlayEnemyReloadCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        _enemyReload.SetActive(true);
        yield return new WaitForSeconds(2f);
        _enemyReload.SetActive(false);
    }
    #endregion

    IEnumerator WaitCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}