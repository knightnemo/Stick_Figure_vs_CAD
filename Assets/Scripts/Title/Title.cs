using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    Renderer rend;
    Color texturecolor;
    float rate;
    float rise;
    // Start is called before the first frame update
    void Start()
    {
        rend= GetComponent<Renderer>();
        texturecolor=new Color(1.0f,1.0f,1.0f);
        rend.material.color = texturecolor;
        rate = 0.1f;
        rise = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(rate<0.1f)
        {
            //rise = 1.0f;
        }
        if(rate>1.0f)
        {
            //rise = -1.0f;
        }
        rate = -0.1f * rise*Time.deltaTime;
        //texturecolor = new Color(texturecolor.r+rate, texturecolor.g+rate, texturecolor.b + rate);
        texturecolor.a += rate;
        rend.material.color = texturecolor;
        Debug.Log("color:" + rend.material.color);
    }
}
