using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logicmaneger : MonoBehaviour
{
    public bool END = false;
    public float endtime = 3.0f;
    float endtimer;
    public bool[] passed;
    // Start is called before the first frame update
    void Start()
    {
        endtimer = endtime;
        passed = new bool[12];
        for(int i=0;i<12;i++)
        {
            passed[i] = false;
        }
        passed[1] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance != null)
        {
            if (playercontroller.instance.rdytoconc)
            {
                END = true;
            }
        }
        if (END)
        {
            if (endtimer > 0)
            {
                endtimer -= Time.deltaTime;
            }
            else
            {
                endtimer = endtime;
                Destroy(gameObject);
            }
        }
    }
}
