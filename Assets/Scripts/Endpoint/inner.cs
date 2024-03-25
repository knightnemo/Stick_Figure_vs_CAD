using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inner : MonoBehaviour
{
    bool activated = false;
    float rotatespeed = 0.6f;

    SpriteRenderer rend;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playercontroller.instance.rdy2 && !playercontroller.instance.End)
        {
            ani.SetTrigger("Open");
        }
        if (playercontroller.instance.End)
        {
            activated = true;
        }
        if (activated)
        {
            transform.Rotate(Vector3.right, 0.6f);
            if (rotatespeed < 4)
            {
                rotatespeed += 0.002f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playercontroller.instance.rdy2)
            {
                ani.SetTrigger("Active");
                playercontroller.instance.End = true;
                rend.material.color = new Color(1, 1.5f, 1);
            }
        }
    }
}
