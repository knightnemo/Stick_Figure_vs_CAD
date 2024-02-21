using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinbar : MonoBehaviour
{
    public float rotatespeed = 0.2f;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotatespeed);
    }
}
