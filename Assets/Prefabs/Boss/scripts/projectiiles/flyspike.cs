using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyspike : MonoBehaviour
{
    float x=0,y=0;
    public float xc=0, yc=0;
    int[]a=new int[5];
    int[]b=new int[5];
    int[] c = new int[5];
    int[] d = new int[5];
    float t=0;

    float flytime = 15.0f;
    float flytimer;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        x = xc;
        y = yc;
        for(int i=0; i<5; i++)
        {
            a[i] = Random.Range(2, 6);
            b[i] =Random.Range(2, 6);
            c[i]=Random.Range(2, 6);
            d[i]=Random.Range(2, 6);
        }

        flytimer = flytime;

        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.6f, 1.05f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 2f);
        if (flytimer > 0)
        {
            flytimer -= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Destroy(gameObject);
        }
        t += Time.deltaTime;
        x = xc;
        y = yc;
        for (int i=0; i < 5; i++)
        {
            x += a[i] * Mathf.Cos(i * t) + b[i] * Mathf.Sin(i * t);
            y += c[i] * Mathf.Cos(i * t) + d[i] * Mathf.Sin(i * t);
        }
        transform.position=new Vector2 (x*0.8f, y*0.8f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EOL")
        {
            EOLcontroller.instance.HP -= 20;
        }
        if (collision.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
        }
    }
}
