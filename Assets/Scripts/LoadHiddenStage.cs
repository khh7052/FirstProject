using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 시작화면에서 히든 스테이지로 이동하는 스크립트
/// 기존의 씬 이동 함수가 메인화면 이동 기능에만 사용불가능하기 때문에 해당 스크립트를 따로 구현
/// 만약 해당 함수를 수정하면 변경하면 될듯함
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
