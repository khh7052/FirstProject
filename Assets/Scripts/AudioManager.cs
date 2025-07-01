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
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
}
