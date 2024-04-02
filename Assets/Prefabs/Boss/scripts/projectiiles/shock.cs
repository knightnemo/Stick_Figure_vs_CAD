using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shock : MonoBehaviour
{
    float staytime = 1.0f;
    float staytimer;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;

        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.8f, 1f, 1f);
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
        }
    }
}
