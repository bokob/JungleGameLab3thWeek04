using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;
    public static SoundManager Instance => _instance;

    [Header("Source")]
    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _effectSource;

    [Header("Clip")]
    [SerializeField] AudioClip _bgm;
    [SerializeField] AudioClip[] _effects;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Init()
    {
        _bgmSource = transform.GetChild(0).GetComponent<AudioSource>();
        _effectSource = transform.GetChild(1).GetComponent<AudioSource>();

        // BGM AudioSource 설정
        _bgmSource.playOnAwake = true;
        _bgmSource.loop = true;
        _bgmSource.volume = 0.2f;
        _bgmSource.clip = _bgm;
    }

    // 배경음 재생
    public void PlayBGM()
    {
        _bgmSource.Play();
    }

    // 효과음 재생
    public void PlayEffect(string effectName)
    {
        for(int i = 0; i < _effects.Length; i++)
        {
            if (_effects[i].name == effectName)
            {
                _effectSource.clip = _effects[i];
                _effectSource.PlayOneShot(_effectSource.clip);
                break;
            }
        }
    }
}