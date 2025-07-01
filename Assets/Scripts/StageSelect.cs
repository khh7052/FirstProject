using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    bool isClearEasy = false;
    bool isClearNormal = false;

    public Button normalBtn;
    public Button hardBtn;

    // 키에 대응화는 int값이 0이면 아직 클리어 하지 않음, 1이라면 클리어한 상황
    string isEasyClearKey = "isEasyClear";
    string isNormalClearKey = "isNormalClear";

    private void Awake()
    {
        // PlayerPrefs의 키와 밸류에 따라 해금 여부를 결정함
        if (PlayerPrefs.HasKey(isEasyClearKey))
            isClearEasy = PlayerPrefs.GetInt(isEasyClearKey) == 1 ? true : false;
        else
            PlayerPrefs.SetInt(isEasyClearKey, 0);

        if (PlayerPrefs.HasKey(isNormalClearKey))
            isClearNormal = PlayerPrefs.GetInt(isNormalClearKey) == 1 ? true : false;
        else
            PlayerPrefs.SetInt(isNormalClearKey, 0);

        PlayerPrefs.Save();
    }

    private void Start()
    {
        // 스테이지 클리어 여부에 따라 스테이지 해금 기능
        if(isClearEasy)
        {
            normalBtn.interactable = true;
        }

        if (isClearNormal)
        {
            hardBtn.interactable = true;
        }
    }

    // 스테이지 이동 함수
    // 전달하는 문자열만 스테이지씬 이름으로 변경하면 해당하는 씬으로 이동가능함
    public void SelectStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }

    // 기능 확인용 함수
    // 이지-> 노말 해금, 노말-> 하드 해금, 하드-> 저장한 키 정보 삭제
    // 정상 작동 확인 완료
    public void DebugSelect(string name)
    {
        if(name == "EASY")
        {
            PlayerPrefs.SetInt(isEasyClearKey, 1);
        }
        else if(name == "NORMAL")
        {
            PlayerPrefs.SetInt(isNormalClearKey, 1);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        Debug.Log($"{name}버튼 클릭");
    }
}
