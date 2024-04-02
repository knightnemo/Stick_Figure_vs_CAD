using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directshot : MonoBehaviour
{
    public float flytime = 5.0f;
    float flytimer;
    public float deg = 0;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        flytimer = flytime;

        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = new Color(2.5f, 1.05f, 1f);
        if (flytimer > 0)
        {
            flytimer -= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Destroy(gameObject);
        }
        
        Vector2 v = new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad));
        Vector2 pos = transform.position;
        pos += v * 0.1f;
        transform.position = pos;
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
            Destroy(gameObject);
        }
        if (collision.tag == "EOL")
        {
            EOLcontroller.instance.HP -= 10;
            Destroy(gameObject);
        }
        if (collision.tag == "shield")
        {
            playercontroller.instance.shieldnum--;
            Destroy(gameObject);
        }
    }

}
