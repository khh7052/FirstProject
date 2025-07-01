using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    public AudioClip mainClip;

    [Header("block")]
    public List<GameObject> block = new();
    public bool blockDown = false;

    [Header("Game Over")]
    public static bool isGameOver = false;
    public bool firstBlock = true;
    public GameObject gameover;
    public AudioClip gameOverClip;

    [Header("Score")]
    public Text scoreText; // 점수 텍스트
    public int score; // 현재 점수

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        CreateBlock();
    }
    private void Start()
    {
        firstBlock = true;
        isGameOver = false;

        AudioManager.Instance.PlayMusic(mainClip, true);
    }

    private void Update()
    {
        if (blockDown)
        {
            Invoke("CreateBlock", 0.5f);
            blockDown = false;
        }
    }

    void CreateBlock()
    {
        int rand = Random.Range(0, block.Count);
        Instantiate(block[rand], new Vector3(block[rand].transform.position.x, 3.5f, block[rand].transform.position.z), Quaternion.identity);
    }

    // 점수 추가
    public void AddScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
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