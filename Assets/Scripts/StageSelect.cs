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

    // Ű�� ����ȭ�� int���� 0�̸� ���� Ŭ���� ���� ����, 1�̶�� Ŭ������ ��Ȳ
    string isEasyClearKey = "isEasyClear";
    string isNormalClearKey = "isNormalClear";

    private void Awake()
    {
        // PlayerPrefs�� Ű�� ����� ���� �ر� ���θ� ������
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
        // �������� Ŭ���� ���ο� ���� �������� �ر� ���
        if(isClearEasy)
        {
            normalBtn.interactable = true;
        }

        if (isClearNormal)
        {
            hardBtn.interactable = true;
        }
    }

    // �������� �̵� �Լ�
    // �����ϴ� ���ڿ��� ���������� �̸����� �����ϸ� �ش��ϴ� ������ �̵�������
    public void SelectStage(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty); // ������ �������� �̸��� ����
        SceneManager.LoadScene("MainScene");
        PlayerPrefs.Save();
    }
    /*
    public void SelectStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }
    */

    // ��� Ȯ�ο� �Լ�
    // ����-> �븻 �ر�, �븻-> �ϵ� �ر�, �ϵ�-> ������ Ű ���� ����
    // ���� �۵� Ȯ�� �Ϸ�
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
        Debug.Log($"{name}��ư Ŭ��");
    }
}
