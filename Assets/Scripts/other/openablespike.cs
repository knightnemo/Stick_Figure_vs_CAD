using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openablespike : MonoBehaviour
{
    public bool active = true;
    public float opentime = 1.0f;
    public float closetime = 2.0f;
    public float timer;

    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        timer = opentime;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
        if (timer <= 0)
        {
            if (active)
            {
                active = false;
                timer = closetime;
                ani.SetBool("IsOpen", false);

            }
            else
            {
                active = true;
                timer = opentime;
                ani.SetBool("IsOpen", true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.tag == "Player")
            {
                playercontroller.instance.ChangeHP(-1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.tag == "Player")
            {
                playercontroller.instance.ChangeHP(-1);
            }
        }
    }
}
