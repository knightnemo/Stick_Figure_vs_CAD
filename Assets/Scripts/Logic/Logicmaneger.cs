using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logicmaneger : MonoBehaviour
{
    public bool END = false;
    public float endtime = 3.0f;
    float endtimer;
    // Start is called before the first frame update
    void Start()
    {
        endtimer = endtime;
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
