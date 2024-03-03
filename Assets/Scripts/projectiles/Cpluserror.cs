using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cpluserror : MonoBehaviour
{
    public float flytime = 3.0f;
    float flytimer;
    public float changetime = 0.05f;
    float changetimer;
    public float firepower = 900.0f;
    bool canlaunch = true;
    Rigidbody2D rigidbody2d;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        flytimer = flytime;
        changetimer = changetime;
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flytimer > 0)
        {
            flytimer -= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Destroy(gameObject);
        }
        if (changetimer > 0)
        {
            changetimer -= Time.deltaTime;
        }
        else
        {
            ani.SetTrigger("Change");
            changetimer = changetime;
        }
        if (canlaunch)
        {
            launch();
            canlaunch = false;
        }

    }
    
    public void launch()
    {
        Vector3 v = (playercontroller.instance.transform.position - transform.position).normalized;
        transform.right = v;
        rigidbody2d.AddForce(v * firepower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = (playercontroller.instance.transform.position - transform.position).normalized;
            transform.right = direction;
            playercontroller.instance.ChangeHP(-1);
            playercontroller.instance.Konckback(direction, 200);
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "erase")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "shield")
        {
            playercontroller.instance.shieldnum--;
            Destroy(gameObject);
        }
    }
}
