using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    Renderer rend;
    Color texturecolor;
    float color=1.0f;
    bool detonate = false;

    public GameObject blast;
    bool canblast = true;

    public GameObject warn;
    bool canwarn = true;
    float distance = 20.0f;

    float detonatetime = 3.0f;
    float detonatetimer;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<Renderer>();
        detonatetimer = detonatetime;
    }

    // Update is called once per frame
    void Update()
    {
        Boom();
        Vector2 d=playercontroller.instance.transform.position-transform.position;
        if(d.magnitude > distance )
        {
            canwarn = true;
        }

        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Mathf.Abs(mousepos.x-transform.position.x)<0.78f && Mathf.Abs(mousepos.y - transform.position.y) < 0.8f)
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                if (canwarn)
                {
                    GameObject warning = Instantiate(warn, transform.position+9*Vector3.right, Quaternion.identity);
                    canwarn = false;
                }
            }
            
        }

        if (playercontroller.instance.candetonate)
        {
            if (detonatetimer > 0)
            {
                detonatetimer -= Time.deltaTime;
            }
            else
            {
                detonate = true;
            }
        }
    }

    void Boom()
    {
        if (detonate)
        {
            color += 0.01f;
            texturecolor = new Color(color, color, color);
            rend.material.color = texturecolor;
            if (color > 6)
            {
                if(canblast) 
                {
                    GameObject flare = Instantiate(blast, transform.position, Quaternion.identity);
                    canblast = false;
                }
               
            }
        }
    }

    
}
