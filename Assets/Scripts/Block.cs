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

    public AudioClip hitClip; // 충돌 사운드

    Rigidbody2D rb;
    BoxCollider2D boxCol;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        isHit = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCol.enabled = false;
    }

    void Update()
    {
        if (!isClicked)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x < xMin || transform.position.x > xMax)
            {
                speed *= -1;
                if(transform.position.x > xMax) transform.position = new Vector3(xMax, transform.position.y, 0);
                if(transform.position.x < xMin) transform.position = new Vector3(xMin, transform.position.y, 0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
                boxCol.enabled = true;
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
        if (!isClicked) return;

        AudioManager.Instance.PlayOneShot(hitClip);

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (TowerManager.instance.firstBlock)
            {
                TowerManager.instance.firstBlock = false;
                TowerManager.instance.AddScore();
                isHit = true;
                return;
            }
            TowerManager.instance.GameOver();
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            if (isHit) return;
            if (collision.gameObject.GetComponent<Block>().isClicked == false) return;
            isHit = true;
            if(transform.position.y > Camera.main.transform.position.y)
                Camera.main.GetComponent<CameraFollow2D>().SetCameraTarget(transform);
            TowerManager.instance.AddScore();
        }
    }

    /// <summary>
    /// 타워 매니저에서 블럭을 생성하면서 해당 블럭의 속도 설정 함수를 호출하면 됨 / 
    /// 속도 설정 함수 => 
    /// 현재 속도 = 초기 속도 * (1 + 0.05*스코어)
    /// </summary>
    /// <param name="score">현재 점수</param>
    public void SetSpeed(int score)
    {
        this.speed = speed * (1 + 0.05f * score); 
    }
}
