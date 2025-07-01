using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject block;

    private void Awake()
    {
        Instantiate(block, new Vector3(block.transform.position.x, 3.5f, block.transform.position.z), Quaternion.identity);
    }
}