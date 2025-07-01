using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public AudioClip startSceneBgmClip; // Inspector���� ����ȭ�� BGM ����

    // �� ȣ��� ��ư �Լ�
    public void LoadScene(string sceneName)
    {
        // �� �̵� ���� ����ȭ�� BGM���� ��ȯ
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
