using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public GameObject gameClearPanel; // ���� �� �г�
    public GameObject gameOverPanel; // ���� �� �г�
    public GameObject board; // ���� ������Ʈ

    AudioSource audioSource;
    public AudioClip clip;

    public int cardCount = 0;
    float time = 0f;
    public float endTime = 30f; // ���� ������ �ð�
    public TeamInfoPanel teamInfoPanel; // �� ���� �г�
    public TeamData[] teamData; // �� ������ �迭


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
        else
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
