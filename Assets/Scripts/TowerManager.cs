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