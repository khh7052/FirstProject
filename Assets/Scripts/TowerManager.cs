using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    [Header("block")]
    public List<GameObject> block = new();
    public bool blockDown = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        CreateBlock();
    }

    private void Update()
    {
        if (blockDown)
        {
            Invoke("CreateBlock", 0.5f);
            blockDown = false;
        }
    }

    void CreateBlock()
    {
        int rand = Random.Range(0, block.Count);
        Instantiate(block[rand], new Vector3(block[rand].transform.position.x, 3.5f, block[rand].transform.position.z), Quaternion.identity);
    }
}