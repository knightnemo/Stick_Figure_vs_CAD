using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arraybar : MonoBehaviour
{
    Vector2 breakpos;
    public float droptime = 1.0f;
    float droptimer;
    bool broken = false;
    Rigidbody2D rigidbody2d;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        droptimer = droptime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playercontroller.instance.rdyforbreak == 1)
            {
                breakpos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (broken)
        {
            if (droptimer > 0)
            {
                droptimer -= Time.deltaTime;
            }
            else
            {
                rigidbody2d.constraints = RigidbodyConstraints2D.None;
                rigidbody2d.gravityScale = 1.0f;
                Vector2 direction = new Vector2(transform.position.x - breakpos.x, transform.position.y - breakpos.y).normalized;
                rigidbody2d.AddForce(direction * 50);

                droptimer = droptime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Breakdown")
        {
            broken = true;
        }
        if(collision.tag == "Breakdowndetect")
        {
            ani.SetTrigger("Detected");
        }
    }
}
