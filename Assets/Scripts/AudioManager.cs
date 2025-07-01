using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void PlayMusic(AudioClip newClip, bool forceRestart = false)
    {
        if (audioSource.clip == newClip && !forceRestart) return; // 이미 재생 중이면 무시

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlatyOneShotMusic(AudioClip newClip)
    {
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null) return; // 클립이 null인 경우 무시
        audioSource.PlayOneShot(clip);
    }

}
