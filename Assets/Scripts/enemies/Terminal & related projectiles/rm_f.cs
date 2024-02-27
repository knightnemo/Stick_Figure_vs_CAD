using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rm_f : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Transform targetTransform;
    Transform rmTransform;
    public GameObject ply;
    public float force = 300.0f;
    public float flytime = 5.0f;
    float timer;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = flytime;
        Debug.Log(direction.x+"/"+direction.y);
        launch();
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

    public void launch()
    {
        Vector3 v=(playercontroller.instance.transform.position-transform.position).normalized;
        Debug.Log(v.x+"/"+v.y+"/"+v.z);
        rigidbody2d.AddForce(v * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        Debug.Log("RM collided with"+collision.gameObject.tag);
        Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "erase")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "erase"){
            Destroy(gameObject);
        }
    }

    public void selfdestroy()
    {
        Destroy(gameObject);
    }
}
