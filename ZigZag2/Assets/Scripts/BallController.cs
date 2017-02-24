using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    [SerializeField]
    private float speed;
    bool started;
    bool gameover;
    Rigidbody rb;
    public GameObject particle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start() {
        started = false;
        gameover = false;
    }

    // Update is called once per frame
    void Update() {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;
                GameManager.instance.StartGame();
            }
        }

        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameover = true;
            rb.velocity = new Vector3(0, -25f, 0);
            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            GameManager.instance.GameOver();
        }
        if (Input.GetMouseButtonDown(0) && !gameover)
        {
            SwitchDirection();
        }
    }

    void SwitchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        } else if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector3(0, 0, speed);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "diamond")
        {
            GameObject part = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            ScoreManagerScript.instance.score += 5;
            Destroy(other.gameObject);
            Destroy(part, 1f);
            
        }
    }
}
