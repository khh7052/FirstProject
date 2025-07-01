using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Test Instance;
    public GameObject gameover;
    public AudioClip gameOverClip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        gameover.SetActive(true);
        if (AudioManager.Instance != null && gameOverClip != null)
        {
            AudioManager.Instance.PlayMusic(gameOverClip);
        }

    }

}
