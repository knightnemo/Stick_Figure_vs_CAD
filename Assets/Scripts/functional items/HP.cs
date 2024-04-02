using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    float movespeed = 0.5f;
    float movetime = 1.0f;
    float movetimer;
    int direction = 1;
    public int num = 1;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        movetimer = movetime;
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1, 1.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (movetimer > 0)
        {
            movetimer -= Time.deltaTime;
        }
        else
        {
            direction = direction * -1;
            movetimer = movetime;
        }
        pos.y = pos.y + direction * movespeed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playercontroller.instance.HP < 5)
            {
                playercontroller.instance.ChangeHP(num);
                playercontroller.instance.MakeSound(8);
                Destroy(gameObject);
            }
        }
    }
}
