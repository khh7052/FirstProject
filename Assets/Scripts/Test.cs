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

    // ���ӿ���
    public void GameOver()
    {
        if (isGameOver) return;
        // ���ӿ��� UI Ȱ��ȭ
        gameover.SetActive(true);
        
        // ���ӿ��� ���� ���
        if (AudioManager.Instance != null && gameOverClip != null)
        {
            isGameOver = true;
            AudioManager.Instance.PlatyOneShotMusic(gameOverClip);
        }
    }

}
