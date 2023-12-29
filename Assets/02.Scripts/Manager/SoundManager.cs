using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _bgmAudioSource;
    private AudioSource _sfxAuidoSource;

    private Dictionary<string, AudioClip> _bgm;
    private Dictionary<string, AudioClip> _sfx;

    private void Awake()
    {
        _bgmAudioSource = GetComponent<AudioSource>();
        _sfxAuidoSource = GetComponent<AudioSource>();
    }

    private void Start()
    { 
        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();

        _bgmAudioSource.loop = true;
        _bgmAudioSource.volume = 0.2f;
        _sfxAuidoSource.volume = 0.3f;
        _sfxAuidoSource.playOnAwake = false;

        // SFX
        _sfx.Add("PlayerAttack1", Resources.Load<AudioClip>("Sound/SFX/PlayerAttack1"));
        _sfx.Add("PlayerAttack2", Resources.Load<AudioClip>("Sound/SFX/PlayerAttack2"));
        _sfx.Add("PlayerAttack3", Resources.Load<AudioClip>("Sound/SFX/PlayerAttack3"));
        _sfx.Add("PlayerSkill", Resources.Load<AudioClip>("Sound/SFX/PlayerSkill"));
        _sfx.Add("EnemyAttack", Resources.Load<AudioClip>("Sound/SFX/EnemyAttack"));
    }

    public void SFX(string name)
    {
        _sfxAuidoSource.PlayOneShot(_sfx[name]);
    }

    public void BGM(string name)
    {
        _bgmAudioSource.Stop();
        _bgmAudioSource.clip = _bgm[name];
        _bgmAudioSource.Play();
    }
}
