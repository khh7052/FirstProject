using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public AudioClip startSceneBgmClip; // Inspector���� ����ȭ�� BGM ����
    public AudioClip stageStartBgmClip; // Inspector���� �������� ȭ�� BGM ����

    // �� ȣ��� ��ư �Լ�
    public void LoadScene(string sceneName)
    {
        if ((sceneName == "StartScene" || sceneName == "StageSelectScene")
            && AudioManager.Instance != null && startSceneBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(startSceneBgmClip, true);
        }

        if (sceneName == "StartScene")
        {
            SceneManager.LoadScene("StartScene");
        } 
        else
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        
    }

    public void Retry()
    {
        if (AudioManager.Instance != null && stageStartBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(stageStartBgmClip, true);
        }
        SceneManager.LoadScene("MainScene");
    }
}