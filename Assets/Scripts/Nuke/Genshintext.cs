using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genshintext : MonoBehaviour
{
    SpriteRenderer rend;
    Color texturecolor;
    float color = 0;
    float endtime = 10;
    float endtimer;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1, 1, 1, 0);
        endtimer = endtime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos=playercontroller.instance.transform.position;
        transform.position = pos;
        if (color <1)
        {
            color += 0.2f*Time.deltaTime;
            texturecolor = new Color(1, 1, 1, color);
            rend.material.color=texturecolor;
        }
        if (endtimer > 0)
        {
            endtimer -= Time.deltaTime;
        }
        else
        {
            playercontroller.instance.rdytoconc = true;
        }
    }
}
