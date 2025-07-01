using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject block;

    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BlockDown(block);
        }
    }

    void BlockDown(GameObject nowBlock)
    {
        transform.position += Vector3.down * speed;
    }
}
