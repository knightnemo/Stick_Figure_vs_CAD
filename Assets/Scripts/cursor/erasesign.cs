using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erasesign : MonoBehaviour
{
    public float showtime = 0.3f;
    float showtimer;
    // Start is called before the first frame update
    void Start()
    {
        showtimer = showtime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(showtimer > 0)
        {
            showtimer -= Time.deltaTime;
        }
        else
        {
            showtimer = showtime;
            gameObject.SetActive(false);
        }
    }
}
