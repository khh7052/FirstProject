using System.Collections;
using System.Collections.Generic;
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

    [Header("Game Over")]
    public static bool isGameOver = false;
    public bool firstBlock = true;
    public GameObject gameover;
    public AudioClip gameOverClip;

    [Header("Score")]
    public Text scoreText; // 점수 텍스트
    public int score; // 현재 점수

    private bool isCreatingBlock = false; // 블록 중복 생성 방지

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        CreateBlock();
    }

    private void Start()
    {
        firstBlock = true;
        isGameOver = false;

        if (AudioManager.Instance != null && mainClip != null)
            AudioManager.Instance.PlayMusic(mainClip);
    }

    private void Update()
    {
        if (blockDown && !isCreatingBlock)
        {
            isCreatingBlock = true;
            Invoke(nameof(CreateBlock), 0.5f);
            blockDown = false;
        }
    }

    void CreateBlock()
    {
        if (isGameOver) return;

        int rand = Random.Range(0, block.Count);

        // 블록 생성 위치: 카메라 위쪽에 생성
        Vector3 spawnPos = new Vector3(
            block[rand].transform.position.x,
            Camera.main.GetComponent<CameraFollow2D>().transform.position.y + 4.5f,
            block[rand].transform.position.z
        );

        Instantiate(block[rand], spawnPos, Quaternion.identity);

        isCreatingBlock = false;
    }

    // 점수 추가
    public void AddScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text가 할당되어 있지 않습니다!");
        }
    }

    // 게임오버
    public void GameOver()
    {
        if (isQuitting) return;
        if (isGameOver) return;

        isGameOver = true;

        // 게임오버 UI 활성화
        if (gameover != null)
            gameover.SetActive(true);

        // 게임오버 사운드 재생
        if (AudioManager.Instance != null && gameOverClip != null)
        {
            AudioManager.Instance.PlatyOneShotMusic(gameOverClip); // 오타 수정은 하지 않음
        }
    }

    // 게임 or 에디터 종료중일때
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}