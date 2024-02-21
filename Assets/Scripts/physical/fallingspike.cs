using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingspike : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rigidbody2d.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         
            if (collision.gameObject.tag == "Player")
            {
                playercontroller.instance.Destroyed();
            }
        
    }
}
