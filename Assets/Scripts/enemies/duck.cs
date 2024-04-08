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
    public int damage = 1;
    Rigidbody2D rigidbody2d;

    public int HP = 3;

    Animator ani;
    public AudioClip[] clips;
    AudioSource aud;
    
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = movetime;
        deadtimer = deadtime;
        aud = GetComponent<AudioSource>();
        
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
            dead = true;
            deadtimer = deadtimer - Time.deltaTime;
            if (dead)
            {
                aud.clip = clips[1];
                aud.Play();
                dead = false;
            }

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
            playercontroller.instance.ChangeHP(-damage);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "erase")
        {
            HP--;
            LogicScript.instance.finalScore++;
            Debug.Log("Duck is hit!");
            aud.clip = clips[0];
            aud.Play();
        }
        if (collision.tag == "shield")
        {
            Debug.Log("Duck is blocked!");
            Vector2 direction=(transform.position - playercontroller.instance.transform.position).normalized;
            rigidbody2d.AddForce(direction * 650.0f);
        }
    }
}
