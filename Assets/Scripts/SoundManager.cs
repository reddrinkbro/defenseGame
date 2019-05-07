using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    void Awake()
    {
        if (instance == null) instance = this;
    }

	public void playSound(AudioClip clip)
    {
        sfxAudioSource.clip = clip;
        sfxAudioSource.Play();
    }
    public void bgmSoundChange(AudioClip clip)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    public void volumeControl(bool isMusic, float volume)
    {
        if(isMusic)
        {
            musicAudioSource.volume = volume;
        }
        else
        {
            sfxAudioSource.volume = volume;
        }
    }

    public void loopOff()
    {
        musicAudioSource.loop = false;
    }
}
