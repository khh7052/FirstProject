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
        // 씬 이름이 "StartScene"일 때만 BGM을 바꾼다!
        if (sceneName == "StartScene" && AudioManager.Instance != null && startSceneBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(startSceneBgmClip, true);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void Retry()
    {
        // 스테이지 시작 브금 다시 재생
        if (AudioManager.Instance != null && stageStartBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(stageStartBgmClip, true);
        }
        SceneManager.LoadScene("MainScene");
    }
}