using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour
{
    public float activedistance = 4.0f;
     bool startcharge = false;
     bool cooling = false;
    public float chargetime = 3.0f;
     float chargetimer;
    public float cooltime = 2.0f;
    float cooltimer;
    // Start is called before the first frame update
    void Start()
    {
        chargetimer = chargetime;
        cooltimer = cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = playercontroller.instance.transform.position - transform.position;
        float distanceX = Mathf.Abs(distance.x);
        float distanceY = Mathf.Abs(distance.y);
        if(distanceX < activedistance && distanceY<4.0f)
        {
            if (!cooling)
            {
                startcharge = true;
            }
            
        }
        if(startcharge)
        {
            if (chargetimer > 0)
            {
                chargetimer-=Time.deltaTime;
            }
            else
            {
                Debug.Log("Teleport!");
                if (distanceX < activedistance)
                {
                    Mirror();
                }
                    
                chargetimer = chargetime;
                cooling = true;
                startcharge = false;

            }
        }
        if (cooling)
        {
            if (cooltimer > 0)
            {
                cooltimer -= Time.deltaTime;
            }
            else
            {
                cooling = false;
                cooltimer = cooltime;
            }
        }
    }

    void Mirror()
    {
        Vector2 pos = new Vector2(transform.position.x*2-playercontroller.instance.transform.position.x,playercontroller.instance.transform.position.y);
        playercontroller.instance.transform.position = pos;
    }
}
