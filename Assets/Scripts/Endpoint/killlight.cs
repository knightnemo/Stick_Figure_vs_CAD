using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killlight : MonoBehaviour
{
    float staytime = 4.5f;
    float staytimer;

    float scale = 0.4f;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;

        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(2.3f, 1.4f, 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (staytimer > 0)
        {
            staytimer -= Time.deltaTime;
        }
        else
        {
            staytimer = staytime;
            Destroy(gameObject);
        }

        if (staytimer > 3)
        {
            if (scale < 1)
            {
                scale += 0.003f;
                transform.localScale = new Vector3(scale*2, scale*60, 0);
            }

        }

        if (staytimer < 1.5f)
        {
            if (scale > 0)
            {
                scale -= 0.003f;
                transform.localScale = new Vector3(scale * 2, scale*60, 0);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            Bosscontroller.instance.HP = 0;
        }
    }
}
