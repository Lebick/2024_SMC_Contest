using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip cilp, float volume = 1f)
    {
        audioSource.PlayOneShot(cilp, volume);
    }
}
