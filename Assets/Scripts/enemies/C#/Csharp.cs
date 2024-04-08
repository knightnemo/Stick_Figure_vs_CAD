using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csharp : MonoBehaviour
{
    public float speed = 2.0f;
    public float movetime = 3.0f;
    float movetimer;
    float speedy = 0.0f;
    public float uptime = 1.0f;
    float uptimer;
    int direction = 1;
    int updirection = 1;

    
    public bool rdyfordive = false;
    public bool pullingup = false;
    public bool finishpullup = false;
    float height;
    public float divedistance = 9.0f;
    public float firedistance = 4.0f;
    public float divetime = 10.0f;
    public float divetimer;

    public int HP = 1;
    bool disabled = false;
    public GameObject bomb;
    
    Rigidbody2D rigidbody2d;
    AudioSource audio;
    public AudioClip[] clips;
    bool cancrash = true;
    bool canfire = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        height = transform.position.y;
        movetimer = movetime;
        divetimer = divetime;
        audio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP<=0)
        {
            disabled = true;
            Crash();
            return;
        }

        if (!rdyfordive && !pullingup)
        {
            Move();
        }
        Vector3 distance= playercontroller.instance.transform.position - transform.position;
        float distanceX = Mathf.Abs(distance.x);
        if (distance.magnitude <= divedistance)
        {
            
            
            if (!finishpullup)
            {
                rdyfordive = true;
                
            }
            
        }
        else
        {
            rdyfordive = false;
            //audio.Stop();
            //Debug.Log("Stop:" + audio.name);
        }
        if(rdyfordive && !pullingup)
        {
            Dive();
            //Debug.Log("Dive!");
            if (Mathf.Abs(distance.y) <= firedistance)
            {
                Fire();
                //Debug.Log("Fire!");
                rdyfordive = false;
                pullingup = true;
                canfire = false;

            }
        }
        if (pullingup)
        {
            Pullup();
            //Debug.Log("Pullup!");
            if (transform.position.y >= height)
            {
                pullingup = false;
                
            }
            if (transform.position.y >= height-0.5f)
            {
                
                finishpullup = true;
                rdyfordive = false;
            }
        }

        if (finishpullup)
        {
            if (divetimer > 0)
            {
                divetimer -= Time.deltaTime;
            }
            else
            {
                finishpullup = false;
                divetimer = divetime;
                canfire = true;
            }
            
        }

    }

    void Move()
    {
        
        rigidbody2d.velocity = new Vector2(speed * direction, speedy);
        if (movetimer > 0)
        {
            movetimer -= Time.deltaTime;
        }
        else
        {
            direction = direction * -1;
            movetimer = movetime;
        }
        if (uptimer > 0)
        {
            uptimer -= Time.deltaTime;
        }
        else
        {
            
            updirection = updirection * -1;
            if (updirection == 1)
            {
                speedy = 0.6f;
            }
            else
            {
                speedy = -0.6f;
            }
            
            uptimer = uptime;
        }
        if (direction == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (direction == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    void Dive()
    {
        
        Vector2 direction= new Vector2(-transform.position.x +
            playercontroller.instance.transform.position.x, -transform.position.y +
            playercontroller.instance.transform.position.y+0.1f);
        direction.Normalize();
        transform.right = direction;
        rigidbody2d.velocity = direction * 6.0f;
    }

    void Pullup()
    {
        int d = 1;
        if (rigidbody2d.velocity.x >= 0)
        {
            d = 1;
        }
        else
        {
            d = -1;
        }
        float h = height - transform.position.y;
        Vector2 direction = new Vector2(h*2.0f*d ,h );
        direction.Normalize();
        transform.right = direction;
        rigidbody2d.velocity = direction * 5.0f;
    }

    void Fire()
    {
        if (canfire)
        {
            audio.clip = clips[1];
            audio.Play();
        }
        for (int i=1;i<=2;i++)
        {
            
            GameObject bombobject = Instantiate(bomb, rigidbody2d.position + Vector2.down * 0.8f, Quaternion.identity);
        }
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "erase")
        {
            HP--;
            LogicScript.instance.finalScore++;
            if (cancrash)
            {
                audio.clip = clips[0];
                audio.Play();
                cancrash = false;
            }
        }
        if (collision.tag == "Player")
        {
            HP--;

            playercontroller.instance.ChangeHP(-1);
            Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
            distance.Normalize();
            playercontroller.instance.Konckback(-distance, 400);
        }
        if (collision.tag == "ground" || collision.tag=="Array")
        {
            if (disabled)
            {
                Destroy(gameObject); 
            }
        }
    }

    void Crash()
    {
        rigidbody2d.gravityScale=1.0f;
        transform.Rotate(Vector3.forward, 3.0f);
        
    }
}
