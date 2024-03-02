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
    int shieldnum=0;

    Vector2 direction;

    public Vector3 v;
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
        //=(playercontroller.instance.transform.position-transform.position).normalized;
        //Debug.Log(v.x+"/"+v.y+"/"+v.z);
        rigidbody2d.AddForce(v * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        Debug.Log("WQ collided with"+collision.gameObject.tag);
        
        if (collision.gameObject.tag == "Player")
        {   
            if(shieldnum==0){
            playercontroller.instance.HP=0;}
            else{
                shieldnum-=1;
            }//FIXME:为后续护盾留的接口
            Debug.Log("Player got hit by death beam");
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "erase"){
            Destroy(gameObject);
        }
        Destroy(gameObject);
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
