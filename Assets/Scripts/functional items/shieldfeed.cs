using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldfeed : MonoBehaviour
{
    public float cooldowntime = 5.0f;
    float cooldowntimer;
    bool rdy = false;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        cooldowntimer = cooldowntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldowntimer > 0)
        {
            cooldowntimer -= Time.deltaTime;
        }
        else
        {
            rdy = true;
            ani.SetBool("rdy", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && rdy)
        {
            if (playercontroller.instance.shieldnum <= 10)
            {
                playercontroller.instance.shieldnum += 4;
                if (playercontroller.instance.shieldnum > 10)
                {
                    playercontroller.instance.shieldnum = 10;
                }
                playercontroller.instance.MakeSound(8);
                rdy = false;
                ani.SetBool("rdy", false);
                cooldowntimer = cooldowntime;
            }
        }
    }
}
