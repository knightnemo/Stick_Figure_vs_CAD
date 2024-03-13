using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float triggerdistance = 4.0f;
    public bool up = false;
    

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        rigidbody2d.gravityScale = 0;
    }

    void Update()
    {
        
        if (up)
        {
            if(playercontroller.instance.transform.position.y-transform.position.y <= triggerdistance && playercontroller.instance.transform.position.y - transform.position.y >0) 
            {
                if (Mathf.Abs(playercontroller.instance.transform.position.x - transform.position.x) < 2f)
                {
                    Debug.Log("Triggered fallingblock");
                    rigidbody2d.gravityScale = 1;
                }
                
            }
        }
        else
        {
            if(transform.position.y-playercontroller.instance.transform.position.y<=triggerdistance && transform.position.y - playercontroller.instance.transform.position.y > 0)
            {
                if (Mathf.Abs(playercontroller.instance.transform.position.x - transform.position.x) < 2f)
                {
                    Debug.Log("Triggered fallingblock");
                    rigidbody2d.gravityScale = 1;
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Triggered fallingblock");
            rigidbody2d.gravityScale = 1;
        }
    }
}