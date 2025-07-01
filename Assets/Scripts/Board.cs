using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public struct BoardData
{
    public int cardPairNum; // ī�� ������ ��
    public int width; // ������ ī�� ��
    public float cardScale; // ī�� ũ��
    public float offset; // ī�� ����
    public float xOffset; // x�� ��ġ ����
    public float yOffset; // y�� ��ġ ����
    
}

public class Board : MonoBehaviour
{
    public GameObject card;
    public int allCardPairNum = 15; // ��ü ī�� ������ ��
    public int cardPairNum = 15; // ���� ���̵����� ���Ǵ� ī�� ������ ��
    public int width = 5; // ������ ī�� ��
    public float cardScale = 1f; // ī�� ũ��
    public float offset = 1.1f; // ī�� ����
    public float xOffset; // x�� ��ġ ����
    public float yOffset; // y�� ��ġ ����

    public void Setting(BoardData data)
    {
        StartCoroutine(CardSet());
    }

    IEnumerator CardSet()
    {
        
        int[] arr = new int[cardPairNum * 2];
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

        GameManager.timeStop = true;

        for (int i = 0; i < arr.Length; i++)
        int length = cardPairNum * 2;
        card.transform.localScale = new Vector3(cardScale, cardScale, 1f); // ī�� ũ�� ����
        for (int i = 0; i < length; i++)
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
        GameManager.Instance.cardCount += length;
    }

}
