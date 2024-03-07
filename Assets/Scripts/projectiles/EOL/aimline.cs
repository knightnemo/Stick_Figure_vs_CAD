using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimline : MonoBehaviour
{
    public float staytime = 1.2f;
    float staytimer;

    Renderer rend;
    Color texturecolor;
    int colortype;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;
        rend = GetComponent<Renderer>();
        colortype = EOLcontroller.instance.spearcolor;
        Recolor();
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

        if (EOLcontroller.instance.spearmode == 1 || EOLcontroller.instance.spearmode == 4)
        {
            transform.right = Vector2.right;
        }
        if (EOLcontroller.instance.spearmode == 2)
        {
            transform.right = Vector2.down;
        }
        if (EOLcontroller.instance.spearmode == 3)
        {
            Vector2 direction1 = new Vector2(1, 1).normalized;
            transform.right = direction1;
        }
       
    }

    void Recolor()
    {

        if (colortype == 1)
        {
            //Red
            texturecolor = new Color(1.5f, 1f, 1f);
        }
        if (colortype == 2)
        {
            //orange
            texturecolor = new Color(1.5f, 1.2f, 1f);
        }
        if (colortype == 3)
        {
            //yellow
            texturecolor = new Color(1.5f, 1.5f, 1f);
        }
        if (colortype == 4)
        {
            //green
            texturecolor = new Color(1f, 1.5f, 1f);
        }
        if (colortype == 5)
        {
            //subgreen
            texturecolor = new Color(1f, 1.5f, 1.2f);
        }
        if (colortype == 6)
        {
            //blue
            texturecolor = new Color(1f, 1f, 1.5f);
        }
        if (colortype == 7)
        {
            //purple
            texturecolor = new Color(1.2f, 1f, 1.5f);
        }

        //Debug.Log("color is " + texturecolor.ToString());
        //rend.material.SetColor("_BloomColor", texturecolor);
        rend.material.color = texturecolor;
    }
}


