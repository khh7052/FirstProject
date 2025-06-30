using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;
    public int cardPairNum = 15; // 카드 종류의 수
    public int width = 5; // 가로줄 카드 수
    public float offset = 1.1f; // 카드 간격

    void Start()
    {
        
        int[] arr = new int[cardPairNum * 2];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i / 2;
            // Debug.Log($"arr[{i}] = {arr[i]}");
        }

        arr = arr.OrderBy(x => Random.Range(0, arr.Length)).ToArray();

        for(int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % width) * offset- 2.2f;
            float y = (i / width) * offset - 4f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount += arr.Length;
    }

}
