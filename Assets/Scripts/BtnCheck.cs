using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCheck : MonoBehaviour
{
    public TeamData.TeamName teamName; // ���� �̸�

    public void CheckBtn()
    {
        // Debug.Log($"{gameObject.name}");
        GameManager.Instance.OpenTeamInfoPanel(teamName);
    }
}
