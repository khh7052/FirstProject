using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    public int cardPairNum = 15; // ī�� ������ ��
    public int width = 5; // ������ ī�� ��
    public float offset = 1.1f; // ī�� ����

    void Start()
    {
        StartCoroutine(CardSet());
    }

    IEnumerator CardSet()
    {
        
        int[] arr = new int[cardPairNum * 2];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i / 2;
            // Debug.Log($"arr[{i}] = {arr[i]}");
        }

        arr = arr.OrderBy(x => Random.Range(0, arr.Length)).ToArray();

        GameManager.timeStop = true;

        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % width) * offset- 2.2f;
            float y = (i / width) * offset - 4f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
            yield return new WaitForSeconds(0.1f);
        }

        GameManager.timeStop = false;
        GameManager.Instance.cardCount += arr.Length;
    }

}
