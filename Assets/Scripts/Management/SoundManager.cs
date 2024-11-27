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

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if(pitch == 1f)
            audioSource.PlayOneShot(clip, volume);
        else
        {
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.pitch = pitch;
            tempSource.PlayOneShot(clip, volume);
            Destroy(tempSource, clip.length);
        }
    }
}
