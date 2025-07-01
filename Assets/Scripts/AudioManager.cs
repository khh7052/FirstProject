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
        if (audioSource.clip == newClip && !forceRestart) return; // �̹� ��� ���̸� ����

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
        if (clip == null) return; // Ŭ���� null�� ��� ����
        audioSource.PlayOneShot(clip);
    }

}
