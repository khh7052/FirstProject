using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public struct BoardData
{
    public int cardPairNum; // 카드 페어 개수
    public int width; // 1줄당 카드 수
    public float cardScale; // 카드 크기
    public float offset; // 카드 간격
    public float xOffset; // x 위치 조정
    public float yOffset; // y 위치 조정
    public Color backgroundColor; // 배경색
}

public class Board : MonoBehaviour
{
    public GameObject card;
    public int cardPairNum = 15; // 카드 페어 개수
    public int width = 5; // 1줄당 카드 수
    public float cardScale = 1f; // 카드 크기
    public float offset = 1.1f; // 카드 간격
    public float xOffset; // x 위치 조정
    public float yOffset; // y 위치 조정

    public void Setting(BoardData data)
    {
        Camera.main.backgroundColor = data.backgroundColor; // 배경색 설정
        StartCoroutine(CardSet(data));
    }

    IEnumerator CardSet(BoardData data)
    {
        cardPairNum = data.cardPairNum;
        width = data.width;
        cardScale = data.cardScale;
        offset = data.offset;
        xOffset = data.xOffset;
        yOffset = data.yOffset;
        int[] arr = new int[cardPairNum * 2]; // 카드 페어 개수 * 2 (짝을 이루는 카드 개수)

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i / 2; // 0, 0, 1, 1, 2, 2, ..., (cardPairNum - 1), (cardPairNum - 1)
            
            // Debug.Log($"arr[{i}] = {arr[i]}");
        }

        // 배열을 무작위로 섞기
        arr = arr.OrderBy(x => Random.Range(0, arr.Length)).ToArray();

        GameManager.timeStop = true;

        card.transform.localScale = new Vector3(cardScale, cardScale, 1f); // 카드 크기 변경

        // 카드 생성
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % width) * offset + xOffset;
            float y = (i / width) * offset + yOffset;

            go.transform.position = new Vector2(x, y);
            // go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

            go.GetComponent<Card>().Setting(arr[i]);
            yield return new WaitForSeconds(0.1f);
        }

        GameManager.timeStop = false;
        GameManager.Instance.cardCount += arr.Length;

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ClsoeAllCards();
    }

}
