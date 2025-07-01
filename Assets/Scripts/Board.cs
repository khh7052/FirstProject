using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct BoardData
{
    public int cardPairNum; // 카드 종류의 수
    public int width; // 가로줄 카드 수
    public float cardScale; // 카드 크기
    public float offset; // 카드 간격
    public float xOffset; // x축 위치 조정
    public float yOffset; // y축 위치 조정
    
}

public class Board : MonoBehaviour
{
    public GameObject card;
    public int allCardPairNum = 15; // 전체 카드 종류의 수
    public int cardPairNum = 15; // 현재 난이도에서 사용되는 카드 종류의 수
    public int width = 5; // 가로줄 카드 수
    public float cardScale = 1f; // 카드 크기
    public float offset = 1.1f; // 카드 간격
    public float xOffset; // x축 위치 조정
    public float yOffset; // y축 위치 조정

    public void Setting(BoardData data)
    {
        cardPairNum = data.cardPairNum;
        width = data.width;
        cardScale = data.cardScale;
        offset = data.offset;
        xOffset = data.xOffset;
        yOffset = data.yOffset;


        int[] arr = new int[allCardPairNum * 2];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i / 2;
            // Debug.Log($"arr[{i}] = {arr[i]}");
        }

        arr = arr.OrderBy(x => Random.Range(0, arr.Length)).ToArray();

        int length = cardPairNum * 2;
        card.transform.localScale = new Vector3(cardScale, cardScale, 1f); // 카드 크기 설정
        for (int i = 0; i < length; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % width) * offset + xOffset;
            float y = (i / width) * offset + yOffset;

            go.transform.position = new Vector2(x, y);
            // go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount += length;
    }

}
