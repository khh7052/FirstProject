using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    bool isClicked = false;
    bool isHit = false;

    public float xMin = -2.3f;
    public float xMax = 2.3f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isHit = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (!isClicked)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x < xMin || transform.position.x > xMax)
                speed *= -1;
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                TowerManager.instance.blockDown = true;
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (isHit) return;

        TowerManager.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (TowerManager.instance.firstBlock)
            {
                TowerManager.instance.firstBlock = false;
                return;
            }

            TowerManager.instance.GameOver();
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            isHit = true;
        }
    }

}
