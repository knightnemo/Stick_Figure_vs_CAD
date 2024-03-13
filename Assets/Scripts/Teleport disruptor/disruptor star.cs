using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disruptorstar : MonoBehaviour
{
    float rotatespeed = 0.2f;

    float movespeed = 0.5f;
    float movetime = 1.0f;
    float movetimer;
    int direction = 1;

    Renderer rend;
    Color texturecolor;
    float colortime = 2.0f;
    float colortimer;
    int colordirection = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        movetimer = movetime;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotatespeed);

        Vector2 pos = transform.position;
        if (movetimer > 0)
        {
            movetimer -= Time.deltaTime;
        }
        else
        {
            direction = direction * -1;
            movetimer = movetime;
        }
        pos.y = pos.y + direction * movespeed * Time.deltaTime;
        transform.position = pos;

        if (colortimer > 0)
        {
            colortimer -= Time.deltaTime;
        }
        else
        {
            colordirection=colordirection * -1;
            colortimer = colortime;
        }
        texturecolor = new Color(1.3f + colordirection * 0.1f, 1.0f, 1.6f + colordirection * 0.1f);
        rend.material.color = texturecolor;
    }
}
