using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public GameObject endTxt;
    public GameObject timeTxt;
    public GameObject gameOver;

    AudioSource audioSource;
    public AudioClip clip;

    public int cardCount = 0;
    float time = 0f;

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
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
        
        if(time >= 30f)
        {
            GameOver();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        endTxt.SetActive(true);
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        Invoke("GameOverIN", 0.5f);
        timeTxt.SetActive(false);
    }

    void GameOverIN()
    {
        Time.timeScale = 0f;
        Invoke("GameOver", 1);
        gameOver.SetActive(true);
    }

    public void MatchCard()
    {
        if(firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount <= 0)
            {
                EndGame();
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

}
