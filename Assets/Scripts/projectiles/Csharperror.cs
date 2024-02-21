using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csharperror : MonoBehaviour
{
    bool canlaunch = true;
    int direction = 1;

    public float droptime = 0.1f;
    float droptimer;
    bool guided = false;

    public float flytime = 5.0f;
    float flytimer;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); 
        if(transform.position.x-playercontroller.instance.transform.position.x < 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
            //Debug.Log("At right!");
        }

        droptimer = droptime;
        flytimer = flytime;
    }

    // Update is called once per frame
    void Update()
    {
        if (canlaunch)
        {
            Toss(direction*10.0f);
            canlaunch = false;
        }

        if (droptimer > 0)
        {
            droptimer -= Time.deltaTime;
        }
        else
        {
            guided = true;
            droptimer = droptime;
        }

        if(flytimer > 0)
        {
            flytimer -= Time.deltaTime; 
        }
        else
        {
            Destroy(gameObject);
            flytimer = flytime;
        }

        if (guided)
        {
            Guidesys();
        }


        
    }

    public void Toss(float speed)
    {
        rigidbody2d.velocity=new Vector2(speed,rigidbody2d.velocity.y);
    }

    void Guidesys()
    {
        Vector3 v = (playercontroller.instance.transform.position - transform.position).normalized;
        transform.right = v;
        Vector2 direction = new Vector2(v.x, v.y).normalized;
        rigidbody2d.velocity=direction*6;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
            
        }
        if(collision.gameObject.tag!="C#" && collision.gameObject.tag!= "C#error")
        {
            Destroy(gameObject);
        }
        
    }
}
