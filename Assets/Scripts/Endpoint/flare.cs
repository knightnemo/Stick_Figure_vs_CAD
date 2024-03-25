using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flare : MonoBehaviour
{
    public float magtime = 0.01f;
    float magtimer;
    float scale = 0.5f;
    int direction = 1;

    SpriteRenderer rend;
    Color texturecolor;
    float color = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Extend();
        Shine();
    }

    void Extend()
    {
        if (magtimer > 0)
        {
            magtimer -= Time.deltaTime;
        }
        else
        {
            if (scale > 0)
            {
                scale = scale + 0.4f * direction;
            }
            
            if (scale > 6.0f)
            {
                direction = -1;
            }

            magtimer = magtime;
        }
        transform.localScale = new Vector3(scale, scale, 1);

    }

    void Shine()
    {
        if (direction == 1)
        {
            texturecolor = new Color(color, color, color);
            color += 0.5f;
            rend.material.color = texturecolor;
        }
        else
        {
            texturecolor = new Color(color, color, color);
            color -= 0.2f;
            rend.material.color = texturecolor;
        }
    }
}
