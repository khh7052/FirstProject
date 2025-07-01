using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public GameObject gameClearPanel; // 성공 시 패널
    public GameObject gameOverPanel; // 실패 시 패널
    public GameObject board; // 보드 오브젝트

    AudioSource audioSource;
    public AudioClip clip;
    public static bool timeStop;
    public AudioClip failClip;

    public int cardCount = 0;
    float time = 0f;
    public float endTime = 30f; // 게임 끝나는 시간
    public TeamInfoPanel teamInfoPanel; // 팀 정보 패널
    public TeamData[] teamData; // 팀 데이터 배열


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
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

    void EndGame()
    {
        // Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        board.SetActive(false);
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
