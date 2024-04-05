using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDrop : MonoBehaviour
{
    playercontroller player;
    Rigidbody2D rb;
    float distance;
    public float triggerdis;
    // Start is called before the first frame update
    void Start()
    {
        player = playercontroller.instance;
        rb= GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        distance=(transform.position-player.transform.position).magnitude;
        if(distance<triggerdis)
        {
            rb.gravityScale = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.Destroyed();
        }
    }
}
