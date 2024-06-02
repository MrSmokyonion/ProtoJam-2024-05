using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoundManager : MonoBehaviour
{
    #region 싱글톤
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header("Audio Sources")]
    [SerializeField] private AudioSource music_source;
    [SerializeField] private AudioSource sfx_source;
    [SerializeField] private AudioSource sfx_loop_source;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip sfx_ui_button;

    [SerializeField] private List<AudioClip> sfx_plunger_throw;
    
    [SerializeField] private List<AudioClip> sfx_wrench_throw;
    
    [SerializeField] private AudioClip sfx_pipe_fixing;

    [SerializeField] private AudioClip sfx_enemy_spawn;
    [SerializeField] private AudioClip sfx_enemy_Hit;


    private void Start()
    {
        music_source.Play();
    }

    public void PlaySFX(SOUND_LIST _sfx_name, float _volume = 1f)
    {
        sfx_source.volume = _volume;
        switch (_sfx_name)
        {
            case SOUND_LIST.SFX_UI_BUTTON:
                PlaySoundEffect(sfx_ui_button);
                break;
            case SOUND_LIST.SFX_PLUNGER_THROW:
                PlaySoundEffect(sfx_plunger_throw);
                break;
            case SOUND_LIST.SFX_WRENCH_THROW:
                PlaySoundEffect(sfx_wrench_throw);
                break;
            case SOUND_LIST.SFX_PIPE_FIXING:
                PlaySoundEffect(sfx_pipe_fixing);
                break;
            case SOUND_LIST.SFX_ENEMY_SPAWN:
                PlaySoundEffect(sfx_enemy_spawn);
                break;
            case SOUND_LIST.SFX_ENEMY_HIT:
                PlaySoundEffect(sfx_enemy_Hit);
                break;
            default:
                break;
        }
    }

    public void PlaySFXLoop(SOUND_LIST _sfx_name)
    {
        switch (_sfx_name)
        {
            case SOUND_LIST.SFX_PIPE_FIXING:
                sfx_loop_source.clip = sfx_pipe_fixing;
                sfx_loop_source.Play();
                break;
            default:
                break;
        }
    }

    public void StopSFXLoop(SOUND_LIST _sfx_name)
    {
        sfx_loop_source.Stop();
    }
    
    private void PlaySoundEffect(AudioClip _target)
    {
        sfx_source.PlayOneShot(_target);
    }

    private void PlaySoundEffect(List<AudioClip> _targetList)
    {
        int _random = Random.Range(0, _targetList.Count);
        sfx_source.PlayOneShot(_targetList[_random]);
    }

    public enum SOUND_LIST
    { 
        SFX_UI_BUTTON,
        SFX_PLUNGER_THROW,
        SFX_WRENCH_THROW,
        SFX_PIPE_FIXING,
        SFX_ENEMY_SPAWN,
        SFX_ENEMY_HIT
    }
}
