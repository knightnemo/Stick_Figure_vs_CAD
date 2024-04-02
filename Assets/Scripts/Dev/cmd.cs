using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cmd : MonoBehaviour
{
    float magtime = 2;
    float magtimer;
    float killtime=9;
    float killtimer;
    float scale = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        magtimer = magtime;
        killtimer= killtime;
    }

    // Update is called once per frame
    void Update()
    {
        if(magtimer > 0)
        {
            magtimer-= Time.deltaTime;
            if (scale < 1.5f)
            {
                scale += 0.05f;
            }
            transform.localScale = new Vector3(scale, scale, 0);
        }
        if(killtimer > 0)
        {
            killtimer-= Time.deltaTime;
        }
        else
        {
            killtimer = killtime;
            Bosscontroller.instance.ON = true;
            Bosscontroller.instance.HP = 0;
            Destroy(gameObject);
        }
    }
}
