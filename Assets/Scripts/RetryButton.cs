using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public AudioClip startSceneBgmClip; // Inspector에서 시작화면 BGM 연결
    public AudioClip stageStartBgmClip; // Inspector에서 스테이지 화면 BGM 연결

    // 씬 호출용 버튼 함수
    public void LoadScene(string sceneName)
    {
        if ((sceneName == "StartScene" || sceneName == "StageSelectScene")
            && AudioManager.Instance != null && startSceneBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(startSceneBgmClip, true);
        }

        SceneManager.LoadScene(sceneName);
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