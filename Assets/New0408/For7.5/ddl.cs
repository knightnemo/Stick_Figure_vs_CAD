using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ddl : MonoBehaviour
{
    float timer;
    float count=10f;
    float katimer;
    float lastx=0f;
    float newx=1f;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        katimer = 0f;
        transform.position=playercontroller.instance.transform.position-Vector3.right*5;
    }

    // Update is called once per frame
    void Update()
    {
        count = Time.time - timer;
        while(count>10f)
        {
            count -= 10f;
        }
        if(timer<9.5f)
        {
            timer+= Time.deltaTime;
        }
        if(timer>=9.5f)
        {
            timer = 0f;
        }
        transform.position += new Vector3(Time.deltaTime *playercontroller.instance.defaultSpeed*(1f-Mathf.Exp(-count/5)), 0, 0);

        if(katimer<3f)
        {
            if (playercontroller.instance.rigidbody2d.velocityX < 1)
            {
                katimer += Time.deltaTime;
            }
            else
            {
                katimer = 0;
            }
        }
        if(katimer>=3f)
        {
            katimer = 0f;
            lastx = newx;
            newx = playercontroller.instance.transform.position.x;
            transform.position= playercontroller.instance.transform.position- Vector3.right * 4;
            if(Mathf.Approximately(lastx, newx))
            {
                playercontroller.instance.Destroyed();
            }
        }
    }
}
