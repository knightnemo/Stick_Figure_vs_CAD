using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C : MonoBehaviour
{
    public float rotatespeed = 1.2f;
    public float speed = 2.0f;
    public float movetime = 3.0f;
    float movetimer;
    public float jumptime = 2.0f;
    float jumptimer;
    int direction=1;

    bool rdyfordash = false;
    public float firedistance = 8.0f;
    bool candash = true;
    public float dashforce = 15000.0f;

    public float explodedistance = 2.0f;
    public GameObject Cerror;
    Rigidbody2D rigidbody2d;

    public int HP = 2;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        movetimer = movetime;
        jumptimer = jumptime;
        aud=GetComponent<AudioSource>();
        //GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!rdyfordash)
        {
            //Move
            Move();
            if (movetimer > 0)
            {
                movetimer -= Time.deltaTime;
            }
            else
            {
                direction = direction * -1;
                movetimer = movetime;
            }
            transform.Rotate(Vector3.forward, rotatespeed * direction * -1);
            //Some vivid jump
            if (jumptimer > 0)
            {
                jumptimer -= Time.deltaTime;
            }
            else
            {
                rigidbody2d.AddForce(new Vector2(0, 4000));
                jumptimer = jumptime;
            }
        }

        Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
        if(distance.magnitude <= firedistance && transform.position.y -
            playercontroller.instance.transform.position.y<0)
        {
            
            rdyfordash = true;
            Dash();
            candash = false;
        }
        if (distance.magnitude <= explodedistance)
        {
            if (rdyfordash)
            {
                aud.Play();
                Explode();
            }
            
        }
    }

    void Move()
    {
        rigidbody2d.velocity = new Vector2(speed * direction, rigidbody2d.velocity.y);
    }

    void Dash()
    {
        if (candash)
        {
            Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
            distance.Normalize();
            rigidbody2d.gravityScale = 0;
            rigidbody2d.AddForce(-distance * dashforce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            candash = true;
            rdyfordash = false;
            rigidbody2d.gravityScale = 1;
        }
        if (collision.tag == "erase")
        {
            HP--;
           
            if (HP <= 0)
            {
                Explode();
            }
        }
        if(collision.tag=="Player" || collision.tag == "feet")
        {
            aud.Play();
            Explode();
        }
    }

    void Explode()
    {
        aud.Play();
        GameObject Cerrorobject = Instantiate(Cerror, rigidbody2d.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
