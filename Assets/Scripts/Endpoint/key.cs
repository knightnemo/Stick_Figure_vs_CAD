using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    float movespeed = 0.5f;
    float movetime = 1.0f;
    float movetimer;
    int direction = 1;
    public GameObject tip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 0.1f);
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
            playercontroller.instance.rdy1 = true;
            Instantiate(tip, transform.position+Vector3.up*7,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
