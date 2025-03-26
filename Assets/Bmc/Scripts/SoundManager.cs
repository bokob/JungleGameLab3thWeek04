using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;
    public static SoundManager Instance => _instance;

    AudioSource _bgmSource;
    AudioSource _effectSource;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Init();
    }

    void Init()
    {

    }

    // 배경음 재생
    void PlayBGM()
    {

    }

    // 효과음 재생
    void PlayEffect()
    {

    }
}