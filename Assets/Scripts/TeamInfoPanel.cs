using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoPanel : MonoBehaviour
{
    public Text teamNameText; // �� �̸� �ؽ�Ʈ
    public Image[] memberImages; // ���� ���� �̹��� �迭

    public void SetTeamInfo(TeamData teamData)
    {
        // �� �̸� ����
        teamNameText.text = teamData.teamName.ToString();

        // ���� ���� ����
        for (int i = 0; i < memberImages.Length; i++)
        {
            if (i < teamData.sprites.Length && teamData.sprites[i] != null)
            {
                memberImages[i].sprite = teamData.sprites[i];
                memberImages[i].gameObject.SetActive(true); // Ȱ��ȭ
            }
            else
            {
                memberImages[i].gameObject.SetActive(false); // ��Ȱ��ȭ
            }
        }
    }
}
