using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blast : MonoBehaviour
{
    public float magtime = 0.02f;
    float magtimer;
    float scale = 1.0f;

    Renderer rend;
    Color texturecolor;
    float color = 1.0f;
    bool litup = true;
    
    public GameObject text;
    bool cantext = true;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Extend();
        Boom();
        if (scale >= 29)
        {
            if (cantext)
            {
                GameObject genshintext = Instantiate(text, transform.position+Vector3.down*2.0f, Quaternion.identity);
                cantext = false;
            }
        }
    }

    void Extend()
    {
        if (magtimer > 0)
        {
            magtimer -= Time.deltaTime;
        }
        else
        {
            if (scale <= 40.0f)
            {
               scale = scale + 0.4f;
            }
            
            magtimer = magtime;
        }
        transform.localScale = new Vector3(scale, scale, 1);
        
    }

    void Boom()
    {
        if (litup)
        {
            color += 0.3f;
            texturecolor = new Color(color, color, color);
            rend.material.color = texturecolor;
        }
        if (color > 200)
        {
            litup = false;
            texturecolor = new Color(1, 1, 1);
            rend.material.color = texturecolor;
        }
    }
}
