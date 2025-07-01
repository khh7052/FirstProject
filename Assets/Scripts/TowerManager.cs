using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    public Transform nowBlock;

    [Header("Game Over")]
    public static bool isGameOver = false;
    public bool firstBlock = true;
    public GameObject gameover;
    public AudioClip gameOverClip;

    [Header("Score")]
    public Text scoreText; // ���� �ؽ�Ʈ
    public int score; // ���� ����

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

    // ���� �߰�
    public void AddScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // ���ӿ���
    public void GameOver()
    {
        if (isQuitting) return;
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

    // ���� or ������ �������϶�
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}