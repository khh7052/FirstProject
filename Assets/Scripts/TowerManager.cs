using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    public bool isQuitting = false; // ���� ����������

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
    public Text scoreText; // ���� �ؽ�Ʈ
    public int score; // ���� ����

    private bool isCreatingBlock = false; // ��� �ߺ� ���� ����

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

        // ��� ���� ��ġ: ī�޶� ���ʿ� ����
        Vector3 spawnPos = new Vector3(
            block[rand].transform.position.x,
            Camera.main.GetComponent<CameraFollow2D>().transform.position.y + 4.5f,
            block[rand].transform.position.z
        );

        Instantiate(block[rand], spawnPos, Quaternion.identity);

        isCreatingBlock = false;
    }

    // ���� �߰�
    public void AddScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text�� �Ҵ�Ǿ� ���� �ʽ��ϴ�!");
        }
    }

    // ���ӿ���
    public void GameOver()
    {
        if (isQuitting) return;
        if (isGameOver) return;

        isGameOver = true;

        // ���ӿ��� UI Ȱ��ȭ
        if (gameover != null)
            gameover.SetActive(true);

        // ���ӿ��� ���� ���
        if (AudioManager.Instance != null && gameOverClip != null)
        {
            AudioManager.Instance.PlatyOneShotMusic(gameOverClip); // ��Ÿ ������ ���� ����
        }
    }

    // ���� or ������ �������϶�
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}