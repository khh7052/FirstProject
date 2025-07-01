using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ȭ�鿡�� ���� ���������� �̵��ϴ� ��ũ��Ʈ
/// ������ �� �̵� �Լ��� ����ȭ�� �̵� ��ɿ��� ���Ұ����ϱ� ������ �ش� ��ũ��Ʈ�� ���� ����
/// ���� �ش� �Լ��� �����ϸ� �����ϸ� �ɵ���
/// </summary>
public class LoadHiddenStage : MonoBehaviour
{
    public AudioClip mainBgmClip;

    public void LoadScene(string sceneName)
    {
        AudioManager.Instance.PlayMusic(mainBgmClip);
        SceneManager.LoadScene(sceneName);
    }
}
