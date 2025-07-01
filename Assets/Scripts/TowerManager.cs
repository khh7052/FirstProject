using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    public bool isQuitting = false; // 게임 종료중인지

    public AudioClip mainClip;

    [Header("block")]
    public List<GameObject> block = new();
    public bool blockDown = false;
    public Transform nowBlock;

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

        AudioManager.Instance.PlayMusic(mainClip);
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
        Instantiate(block[rand], new Vector3(block[rand].transform.position.x, Camera.main.GetComponent<CameraFollow2D>().transform.position.y + 4.5f, block[rand].transform.position.z), Quaternion.identity);
        
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
        if (isQuitting) return;
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

    // 게임 or 에디터 종료중일때
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}