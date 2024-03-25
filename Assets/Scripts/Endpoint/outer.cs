using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outer : MonoBehaviour
{
    bool activated = false;
    float rotatespeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance.End)
        {
            activated = true;
        }
        if (activated)
        {
            transform.Rotate(Vector3.up, rotatespeed);
            if (rotatespeed < 2)
            {
                rotatespeed += 0.001f;
            }
        }

    }
}
