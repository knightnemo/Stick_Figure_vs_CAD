using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talk1 : MonoBehaviour
{
    float staytime = 19;
    float staytimer;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;
    }

    // Update is called once per frame
    void Update()
    {
        if(staytimer > 0)
        {
            staytimer -= Time.deltaTime;
        }
        else
        {
            staytimer = staytime;
            Destroy(gameObject);
        }
    }
}
