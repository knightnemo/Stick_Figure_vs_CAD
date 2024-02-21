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
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = flytime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;

        }
        else
        {
            timer = flytime;
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
            Destroy(gameObject);
        }
        
    }
}
