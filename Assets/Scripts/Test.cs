using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Test Instance;
    public static bool isGameOver = false;
    public GameObject gameover;
    public AudioClip gameOverClip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isGameOver = false;
    }

    // 게임오버
    public void GameOver()
    {
        if (isGameOver) return;
        // 게임오버 UI 활성화
        gameover.SetActive(true);
        
        // 게임오버 사운드 재생
        if (AudioManager.Instance != null && gameOverClip != null)
        {
            isGameOver = true;
            AudioManager.Instance.PlatyOneShotMusic(gameOverClip);
        }
    }

}
