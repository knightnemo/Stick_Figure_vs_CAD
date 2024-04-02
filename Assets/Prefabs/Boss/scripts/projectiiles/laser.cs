using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    float staytime = 4.0f;
    float staytimer;

    float scale = 0.1f;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;

        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(4.5f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 p=Bosscontroller.instance.transform.position;
        p.y+=16;
        transform.position=p;

        if (staytimer > 0)
        {
            staytimer -= Time.deltaTime;
            transform.Rotate(Vector3.forward, 0.08f);
        }
        else
        {
            staytimer = staytime;
            Destroy(gameObject);
        }

        if (staytimer > 3f)
        {
            if (scale < 1)
            {
                scale += 0.005f;
                transform.localScale=new Vector3 (scale*40, scale, 0);
            }
            
        }
        
        if(staytimer < 1f)
        {
            if (scale > 0)
            {
                scale -= 0.005f;
                transform.localScale = new Vector3(scale*40, scale, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
            Debug.Log("player is hit");
        }
        if (collision.tag == "EOL")
        {
            EOLcontroller.instance.HP -= 50;
        }
    }
}
