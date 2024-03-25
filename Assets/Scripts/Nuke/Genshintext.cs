using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genshintext : MonoBehaviour
{
    SpriteRenderer rend;
    Color texturecolor;
    float color = 0;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos=playercontroller.instance.transform.position;
        transform.position = pos;
        if (color <1)
        {
            color += 0.05f*Time.deltaTime;
            texturecolor = new Color(1, 1, 1, color);
            rend.material.color=texturecolor;
        }
    }
}
