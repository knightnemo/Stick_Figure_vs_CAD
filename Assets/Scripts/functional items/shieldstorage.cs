using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldstorage : MonoBehaviour
{
    float movespeed = 0.5f;
    float movetime = 1.0f;
    float movetimer;
    int direction = 1;
    public int nums = 1;
    // Start is called before the first frame update
    void Start()
    {
        movetimer = movetime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (movetimer > 0)
        {
            movetimer -= Time.deltaTime;
        }
        else
        {
            direction = direction * -1;
            movetimer = movetime;
        }
        pos.y = pos.y+direction*movespeed* Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playercontroller.instance.shieldnum <= 10)
            {
                playercontroller.instance.shieldnum+=nums;
                if(playercontroller.instance.shieldnum > 10)
                {
                    playercontroller.instance.shieldnum = 10;
                }
                playercontroller.instance.MakeSound(8);
                Destroy(gameObject);
            }
        }
    }
}
