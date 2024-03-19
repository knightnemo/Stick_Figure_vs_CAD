using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bosscontroller : MonoBehaviour
{
    public static Bosscontroller instance {  get; private set; }
    public playercontroller player;
    private float timer;
    private float lengthtimer;
    private float distance;
    int lor;
    public float speed = 0.0001f;
    public GameObject bullet;
    Animator ani;
    Rigidbody2D rb;

    //MultiShoot
    private int MultiTrack;//track type of MultiShoot 
    public float MultiInterval=0.0000f,MultiLength=9.0f;

    //Blow
    float x0, y0;
    bool recorded=false;
    public float blowforce = 10f;
    
    bool isMulti = false;
    bool isBlow = false;
    bool isJump = false;
    bool isWalk = false;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        player = playercontroller.instance;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance=player.transform.position.x-transform.position.x;
        if (distance>0) lor = 1;
        else
        {
            lor = -1;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMulti = true;
            lengthtimer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            isBlow = true;
        }
        if (Mathf.Abs(distance) <= 10f&&!isJump)
        {
            isMulti = true;
            lengthtimer = 0f;
        }
        if(Mathf.Abs(distance)>=30f)
        {
            isJump = true;
        }

        if(Mathf.Abs(distance)>10f&&Mathf.Abs(distance)<30f&&isWalk)
        {
            Move();
        }
        else
        {
            ani.SetBool("Walk", false);
        }

        if (isMulti)
        {
            MultiShoot();
        }

        if(isBlow)
        {
            if (!recorded)
            {
                x0 = transform.position.x;
                y0 = transform.position.y;
                recorded = true;
            }
            
            Blow();
        }
        else
        {
            recorded = false;
            ani.SetBool("BlowRight", false);
            ani.SetBool("BlowLeft", false);
        }

        if(isJump)
        {
            Jump();
        }
        else
        {
            ani.SetBool("Jump", false);
            ani.SetBool("Fall", false);
        }
    }
    void Move()
    {
        transform.position += new Vector3((float)lor*speed *Time.deltaTime/2f, 0f, 0f);
        ani.SetBool("Walk", true);
    }

    void Blow()
    {
        
        if(lor==1)
        {
            ani.SetBool("BlowRight", true);
            if(transform.position.x<x0+10f)
            {
                transform.Translate(20f*Vector3.right*Time.deltaTime);
            }
        }
        if(lor==-1)
        {
            ani.SetBool("BlowLeft", true);
            if (transform.position.x > x0 - 10f)
            {
                transform.Translate(20f*Vector3.left * Time.deltaTime);
            }
        }
    }

    void MultiShoot()
    {

        ani.SetBool("MultiShoot",true);
        if (lengthtimer<MultiLength)
        {
            lengthtimer += Time.deltaTime;
            if (timer < MultiInterval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
                {
                    GameObject temp = Instantiate(bullet, transform.position + new Vector3(0f, 16f, 0f), transform.rotation);
                    Circle circle = temp.GetComponent<Circle>();
                    circle.tracktype = MultiTrack++;
                }
                
            }
        }
        else
        {
            isMulti = false;
            ani.SetBool("MultiShoot", false);
        }
    }

    void Jump()
    {
        if(transform.position.y-player.transform.position.y<20f&&rb.velocityY>=0f)
        {
            rb.velocityY = 20f;
        }
        else
        {
            
            if(Mathf.Abs(transform.position.x-player.transform.position.x)>5f)
                transform.Translate(20f*Vector3.right*lor*Time.deltaTime);
            else
            {
                rb.velocityY = -50f;
            }
            //transform.position = new Vector3(player.transform.position.x, transform.position.y, 0);
        }
        if(rb.velocityY>0)
        {
            ani.SetBool("Jump",true);
            ani.SetBool("Fall", false);
        }
        else if(rb.velocityY<0)
        {
            ani.SetBool("Jump", false);
            ani.SetBool("Fall", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isJump)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.rigidbody2d.AddForce(new Vector2(lor * 100f, 0));//»÷·ÉPlayer
                player.ChangeHP(-1);
                
            }
            if(collision.gameObject.tag =="ground")
            {
                isJump= false; 
            }
        }
        if(isBlow)
        {
            if(collision.gameObject.tag=="player")
            {
                player.rigidbody2d.AddForce(new Vector2(lor*blowforce * 1.7f, blowforce));//»÷·ÉPlayer
                isBlow = false;
            }
        }
    }
    /*IEnumerator MultiShooter()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }*/
}
