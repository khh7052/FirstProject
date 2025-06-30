using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoPanel : MonoBehaviour
{
    public Text teamNameText; // 팀 이름 텍스트
    public Image[] memberImages; // 팀원 사진 이미지 배열

    public void SetTeamInfo(TeamData teamData)
    {
        // 팀 이름 설정
        teamNameText.text = teamData.teamName.ToString();

        // 팀원 사진 설정
        for (int i = 0; i < memberImages.Length; i++)
        {
            if (i < teamData.sprites.Length && teamData.sprites[i] != null)
            {
                memberImages[i].sprite = teamData.sprites[i];
                memberImages[i].gameObject.SetActive(true); // 활성화
            }
            else
            {
                memberImages[i].gameObject.SetActive(false); // 비활성화
            }
        }
    }
}
