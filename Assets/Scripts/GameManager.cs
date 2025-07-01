using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.Windows;


[System.Serializable]
public class TeamData
{
    public TeamName teamName; // �� �̸� ������
    // public string name = ""; // ���� �̸�
    public Sprite[] sprites = new Sprite[3]; // ���� ���� 3��


    public enum TeamName
    {
        ������,
        �ֿ쿵,
        ���κ�,
        �賲��,
        ������
    }

}

[System.Serializable]
public class DifficultyData
{
    public Difficulty difficulty; // ���̵�
    public BoardData boardData;
    public float endTime = 30f; // ���� ������ �ð�

}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public GameObject gameClearPanel; // ���� �� �г�
    public GameObject gameOverPanel; // ���� �� �г�
    public Board board; // ���� ������Ʈ

    AudioSource audioSource;
    public AudioClip clip;
    public static bool timeStop;
    public AudioClip failClip;
    public AudioClip mainBgmClip; // ���� ���� ������ ����� ����
    public AudioClip timeAttackClip; // �ð� �˹����� �� ���

    public int cardCount = 0;
    float time = 0f;
    public float endTime = 30f; // ���� ������ �ð�
    public TeamInfoPanel teamInfoPanel; // �� ���� �г�
    public TeamData[] teamData; // �� ������ �迭

    public Difficulty difficulty; // ���� ���̵�
    public DifficultyData[] difficultyData; // ���̵� ������ �迭
    private bool onTimeAttack = false; // �ð� �˹� ����

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (board == null) board = FindObjectOfType<Board>();

    }

    void Start()
    {
        onTimeAttack = false;
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.HasKey("Difficulty"))
        {
            string str = PlayerPrefs.GetString("Difficulty", "Easy");
            difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), str);
        }

        // ������� AudioManager������ ���
        if (AudioManager.Instance != null && mainBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(mainBgmClip);
        }

        board.Setting(difficultyData[(int)difficulty].boardData);
        endTime = difficultyData[(int)difficulty].endTime;

    }

    void Update()
    {
        if (time >= endTime)
        {
            EndGame();
        }
        else if (!timeStop)
        {
            time += Time.deltaTime;
            timeText.text = time.ToString("N2");

            if (endTime - time <= 10f && !onTimeAttack)
            {
                onTimeAttack = true;
                AudioManager.Instance.PlayMusic(timeAttackClip); // �ð� �˹� �� ��� ���
            }
        }
    }

    // ��� ī�� �޸� ���̰� ������
    public void ClsoeAllCards()
    {
        Card[] cards = FindObjectsOfType<Card>();
        foreach (var card in cards)
        {
            card.SetOpenAnim(false);
        }
    }


    void EndGame()
    {
        // Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        board.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }

    void GameClear()
    {
        Time.timeScale = 0f;
        gameClearPanel.SetActive(true);

        // ���� Ŭ���� �� �������� �ر�
        switch (difficulty)
        {
            case Difficulty.Easy:
                PlayerPrefs.SetInt("isEasyClear", 1);
                break;
            case Difficulty.Normal:
                PlayerPrefs.SetInt("isNormalClear", 1);
                break;
            case Difficulty.Hard:
                // PlayerPrefs.SetInt("isHardClear", 1);
                break;
        }

    }

    public void MatchCard()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount <= 0)
            {
                GameClear();
            }
        }
        else
        {
            audioSource.PlayOneShot(failClip);

            firstCard.CloseCard();
            secondCard.CloseCard();

        }

        firstCard = null;
        secondCard = null;
    }

    public void OpenTeamInfoPanel(TeamData.TeamName name)
    {
        teamInfoPanel.gameObject.SetActive(true);
        foreach (var team in teamData)
        {
            if (team.teamName == name)
            {
                teamInfoPanel.SetTeamInfo(team);
                break;
            }
        }
    }

    public void CloseTeamInfoPanel()
    {
        teamInfoPanel.gameObject.SetActive(false);
    }

}
