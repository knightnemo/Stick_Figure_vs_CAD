using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck : MonoBehaviour
{
    public float speed = 1.0f;
    public float movetime = 1.0f;
    public float deadtime = 1.0f;
    float timer;
    float deadtimer;
    public int direction = 1;
    Rigidbody2D rigidbody2d;

    public int HP = 3;

    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = movetime;
        deadtimer = deadtime;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
        else
        {
            direction = direction * -1;
            timer = movetime;
        }

        if (direction == 1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (direction == -1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (HP <= 0)
        {
            ani.SetTrigger("IsKilled");
            deadtimer = deadtimer - Time.deltaTime;

        }
        if (deadtimer <= 0)
        {
            deadtimer = deadtime;
            Destroy(gameObject);
        }
    }

    void Move()
    {
        rigidbody2d.velocity = new Vector2(speed * direction, rigidbody2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "erase")
        {
            HP--;
            Debug.Log("Duck is hit!");
        }
    }
}
