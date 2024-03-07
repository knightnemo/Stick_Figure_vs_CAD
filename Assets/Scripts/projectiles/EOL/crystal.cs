using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal : MonoBehaviour
{
    public float flytime = 6.0f;
    float flytimer;

    public float firedelaytime = 2.0f;
    float firedelaytimer;

    Vector2 hoverpos;
    Vector2 distance;
    Vector2 direction;
    public bool appraoching = true;
    public bool rdyforfire=false;
    public bool fired=false;
    //Renderer
    Renderer rend;
    Material material;
    float colortime = 1.0f;
    int colortype = 1;
    Color texturecolor;
    // Start is called before the first frame update
    void Start()
    {
        flytimer = flytime;
        firedelaytimer = firedelaytime;
        int dx = Random.Range(-20,20);
        int dy = Random.Range(-4,4);
        hoverpos = new Vector2(transform.position.x + dx, transform.position.y + dy);
        
        rend = GetComponent<Renderer>();
        Debug.Log("oricolor is " + rend.material.color);
        Recolor();
    }

    // Update is called once per frame
    void Update()
    {
        Shine();

        if (flytimer > 0)
        {
            flytimer-= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Destroy(gameObject);
        }
        
        if (appraoching)
        {
            distance = new Vector2(hoverpos.x - transform.position.x, hoverpos.y - transform.position.y);
            direction = distance.normalized;
            if (distance.magnitude > 0.5f )
            {
                Vector2 pos = transform.position;
                pos = pos + direction * 0.03f;
                transform.position = pos;
            }
            else
            {
                
                Debug.Log("crystal ready for fire!");
                rdyforfire = true;
                appraoching = false;
                

            }
        }
        if(rdyforfire)
        {
            if(firedelaytimer > 0)
            {
                firedelaytimer -= Time.deltaTime;
            }
            else
            {
                firedelaytimer = firedelaytime;
                fired = true;
                rdyforfire = false;
                direction = (EOLcontroller.instance.target.position - transform.position).normalized;
            }
        }

        if (fired)
        {
            Vector2 facedirection = new Vector2(-direction.y, direction.x);
            transform.right = facedirection;
            Vector2 pos1= transform.position;
            pos1 = pos1 + direction * 0.15f;
            Debug.Log("direction is " + direction);
            transform.position = pos1;
        }
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

    void Shine()
    {
        colortime += Time.deltaTime*0.5f;
        if (colortime > 2f)
        {
            colortime = 1.5f;
        }
        
        if (colortype == 1)
        {
            //Red
             texturecolor = new Color(colortime, 1f, 1f);
        }
        if (colortype == 2)
        {
            //orange
             texturecolor = new Color(colortime, colortime*0.7f, 1f);
        }
        if (colortype == 3)
        {
            //yellow
            texturecolor = new Color(colortime, colortime, 1f);
        }
        if (colortype == 4)
        {
            //green
            texturecolor = new Color(1f, colortime, 1f);
        }
        if (colortype == 5)
        {
            //subgreen
            texturecolor = new Color(1f, colortime, colortime*0.4f);
        }
        if (colortype == 6)
        {
            //blue
            texturecolor = new Color(1f, 1f, colortime);
        }
        if (colortype == 7)
        {
            //purple
            texturecolor = new Color(colortime*0.7f, 1f, colortime);
        }
        
        //Debug.Log("color is "+texturecolor.ToString());
        //rend.material.SetColor("_BloomColor", texturecolor);
        rend.material.color= texturecolor;
        
    }

    void Recolor()
    {
        colortype = Random.Range(1, 7);
    }
}
