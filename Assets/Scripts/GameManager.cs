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
    public TeamName teamName; // 팀 이름 열거형
    // public string name = ""; // 팀원 이름
    public Sprite[] sprites = new Sprite[3]; // 팀원 사진 3개


    public enum TeamName
    {
        김혜현,
        최우영,
        김인빈,
        김남진,
        이형권
    }

}

[System.Serializable]
public class DifficultyData
{
    public Difficulty difficulty; // 난이도
    public BoardData boardData;
    public float endTime = 30f; // 게임 끝나는 시간

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
    public GameObject gameClearPanel; // 성공 시 패널
    public GameObject gameOverPanel; // 실패 시 패널
    public Board board; // 보드 오브젝트

    AudioSource audioSource;
    public AudioClip clip;
    public static bool timeStop;
    public AudioClip failClip;
    public AudioClip mainBgmClip; // 메인 게임 씬에서 재생할 음악

    public int cardCount = 0;
    float time = 0f;
    public float endTime = 30f; // 게임 끝나는 시간
    public TeamInfoPanel teamInfoPanel; // 팀 정보 패널
    public TeamData[] teamData; // 팀 데이터 배열

    public Difficulty difficulty; // 현재 난이도
    public DifficultyData[] difficultyData; // 난이도 데이터 배열

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (board == null) board = FindObjectOfType<Board>();

    }

    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.HasKey("Difficulty"))
        {
            string str = PlayerPrefs.GetString("Difficulty", "Easy");
            difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), str);
        }

        // 배경음은 AudioManager에서만 재생
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
        }
    }

    // 모든 카드 뒷면 보이게 뒤집기
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
