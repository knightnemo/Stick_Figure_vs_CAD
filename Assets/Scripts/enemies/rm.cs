using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rm : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Transform targetTransform;
    Transform rmTransform;
    public GameObject ply;
    public float force = 300.0f;
    public float flytime = 7.0f;
    float timer;
    Vector2 direction;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rmTransform = GetComponent<Transform>();
        targetTransform = ply.transform;
        direction=targetTransform.position-rmTransform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = flytime;
        Debug.Log(direction.x+"/"+direction.y);
        launch(direction);
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
        rigidbody2d.AddForce(-direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
        }
        if (collision.tag == "erase")
        {
            Destroy(gameObject);
        }
    }

    public void selfdestroy()
    {
        Destroy(gameObject);
    }
}
