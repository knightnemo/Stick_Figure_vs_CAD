using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{
    public float flytime = 4.0f;
    float flytimer;
    int flymode = 0;

    public float firedelaytime = 1.0f;
    float firedelaytimer;

    
    Vector2 v;

    Renderer rend;
    Color texturecolor;
    int colortype;
    // Start is called before the first frame update
    void Start()
    {
        flytimer = flytime;
        firedelaytimer = firedelaytime;
        
        flymode = EOLcontroller.instance.spearmode;
        Debug.Log("Spears fly at mode " + flymode);
        if (flymode == 3)
        {
            Vector2 direction1 = new Vector2(1, 1).normalized;
            transform.right = direction1;
        }
        if (flymode == 2)
        {
            transform.right = -Vector2.down;
        }
        if (flymode == 5)
        {
           v = (playercontroller.instance.transform.position - transform.position).normalized;
            
        }

        rend=GetComponent<Renderer>();
        colortype = EOLcontroller.instance.spearcolor;
        Recolor();
    }

    // Update is called once per frame
    void Update()
    {
        if(flytimer > 0)
        {
            flytimer -= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Destroy(gameObject);
        }

        if(firedelaytimer > 0)
        {
            firedelaytimer -= Time.deltaTime;
        }
        else 
        {
            if (flymode >= 0 && flymode <= 4)
            {
                Normalfly();
            }
            if (flymode == 5)
            {
                Aimfly();
            }
             
        }
        
    }

    void Normalfly()
    {
        Vector2 pos=transform.position;
        if(flymode==1 || flymode == 4 ||flymode==0)
        {
            pos.y -= 0.2f;
        }
        if(flymode==2)
        {
            pos.x += 0.2f;
        }
        if(flymode==3)
        {
            pos.x += 0.1f;
            pos.y -= 0.1f;
        }
        transform.position = pos;
    }
    void Aimfly()
    {
        Vector2 pos= transform.position;
        pos = pos + v * 0.1f;
        transform.position = pos;
    }

    //Please change "player"into "boss"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
            Destroy(gameObject);
        }
    }

    void Recolor()
    {
        
        if (colortype == 1)
        {
            //Red
            texturecolor = new Color(2, 1f, 1f);
        }
        if (colortype == 2)
        {
            //orange
            texturecolor = new Color(2, 1.3f, 1f);
        }
        if (colortype == 3)
        {
            //yellow
            texturecolor = new Color(2, 2, 1f);
        }
        if (colortype == 4)
        {
            //green
            texturecolor = new Color(1f, 2, 1f);
        }
        if (colortype == 5)
        {
            //subgreen
            texturecolor = new Color(1f, 2, 1.4f);
        }
        if (colortype == 6)
        {
            //blue
            texturecolor = new Color(1f, 1f, 2);
        }
        if (colortype == 7)
        {
            //purple
            texturecolor = new Color(1.4f, 1f, 2);
        }

        //Debug.Log("color is " + texturecolor.ToString());
        //rend.material.SetColor("_BloomColor", texturecolor);
        rend.material.color = texturecolor;
    }
}
