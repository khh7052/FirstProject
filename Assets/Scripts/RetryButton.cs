using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public AudioClip startSceneBgmClip; // Inspector에서 시작화면 BGM 연결

    // 씬 호출용 버튼 함수
    public void LoadScene(string sceneName)
    {
        // 씬 이동 전에 시작화면 BGM으로 전환
        if (AudioManager.Instance != null && startSceneBgmClip != null)
        {
            AudioManager.Instance.PlayMusic(startSceneBgmClip);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
