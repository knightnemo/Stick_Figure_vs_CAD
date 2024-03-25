using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class erase : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float force = 400.0f;
    public float flytime = 5.0f;
    float timer;
    SpriteRenderer rend;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = flytime;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance.cankillall)
        {
            rend.material.color = new Color(3.5f, 1.2f, 1.2f);
        }
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;

        }
        else
        {
            timer = flytime;
            if (playercontroller.instance.cankillall)
            {
                playercontroller.instance.killall = true;
                playercontroller.instance.cankillall = false;
            }
            Destroy(gameObject);
        }
    }

    public void launch(Vector2 direction)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("erase hit:" + collision.tag);
        if (collision.tag != "Player" && collision.tag!="feet")
        {
            if (playercontroller.instance.cankillall)
            {
                playercontroller.instance.killall = true;
                playercontroller.instance.cankillall = false;
            }
            Destroy(gameObject);

        }
        
    }
}
