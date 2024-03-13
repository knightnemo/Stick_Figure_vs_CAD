using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movespike1 : MonoBehaviour
{
    //Spike will move upward once player is above it

    public bool working = true;
    public bool active = false;
    public float Ymin = 1.1f;
    public float Ymax = 5.0f;
    public float movetime = 1f;
    public float movetimer;


    // Start is called before the first frame update
    void Start()
    {
        movetimer = movetime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (working)
        {
            if(playercontroller.instance.transform.position.y-transform.position.y>=Ymin && playercontroller.instance.transform.position.y - transform.position.y <= Ymax)
            {
                if(playercontroller.instance.transform.position.x-transform.position.x<1f && playercontroller.instance.transform.position.x - transform.position.x > -1f)
                {
                    active = true;
                }
                
            }
        }

        if (active)
        {
            if (movetimer > 0)
            {
                Debug.Log("spike move up!");
               
               
                movetimer -=Time.deltaTime;
            }
            else
            {
                active = false;
                working = false;
                movetimer = movetime;
            }
        }
        if (active)
        {
            Vector2 pos = transform.position;
            pos.y += 0.05f;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.Destroyed();
        }
    }

    
}
