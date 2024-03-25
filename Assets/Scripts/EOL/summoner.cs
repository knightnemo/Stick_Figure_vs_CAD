using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summoner : MonoBehaviour
{
    bool carried = false;
    bool thrown = false;
    Rigidbody2D rigidbody2d;
    public GameObject EOL;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carried && !thrown)
        {
            if (playercontroller.instance.rigidbody2d.velocityX > 0)
            {
                transform.position = playercontroller.instance.transform.position - Vector3.right * 0.5f;
            }
            else
            {
                transform.position = playercontroller.instance.transform.position + Vector3.right * 0.5f;
            }
            
        }
        if (carried && !thrown)
        {
            if(Input.GetKeyDown(KeyCode.Q)) 
            {
                carried = false;
                thrown = true;
                rigidbody2d.gravityScale = 1;
                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = new Vector2(mousepos.x - transform.position.x, mousepos.y - transform.position.y);
                direction.Normalize();
                rigidbody2d.AddForce(direction * 550.0f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!carried && !thrown)
        {
            if (collision.tag == "Player")
            {
                carried = true;
            }
        }
        if (thrown)
        {
            if(collision.tag != "Player")
            {
                Instantiate(EOL, transform.position + Vector3.up * 10, Quaternion.identity);
                Bosscontroller.instance.targettype = 2;
                Destroy(gameObject);
            }
        }
    }
}
