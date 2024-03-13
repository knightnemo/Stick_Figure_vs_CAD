using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingspike : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float triggerdistance = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance.transform.position.y - transform.position.y >= -triggerdistance && playercontroller.instance.transform.position.y - transform.position.y < 0)
        {
            if (Mathf.Abs(playercontroller.instance.transform.position.x - transform.position.x) < 1.3f)
            {
                Debug.Log("Triggered fallingspike");
                rigidbody2d.gravityScale = 1;
            }

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
