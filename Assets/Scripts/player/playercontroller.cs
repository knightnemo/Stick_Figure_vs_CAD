using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    public static playercontroller instance { get; private set; }
    //Movement
    float RT;

    Rigidbody2D rigidbody2d;
    public float speed = 3.0f;
    public bool canjump = true;
    public bool canjumptwice = false;
    public bool intheair;

    //HP
    public int HP = 5;
    public bool died=false;
    public float invtime = 1.0f;
    float invtimer;

    //Teleport
    public int rdyfortele = -1;
    Transform line;


    //Erase
    public GameObject erase;
    public float erasetime = 0.5f;
    float erasetimer;
    Transform erasepointer;
    //Fillet
    public GameObject fillet;
    public float fillettime = 1.0f;
    float fillettimer;
    //Breakdown
    public GameObject detect;
    public int rdyforbreak = -1;
    //Animation
    Animator ani;

    //技能能否使用，初始化为false
    public bool canErase=true;
    public bool canTeleport=true;
    public bool canBreakdown=true;
    public bool canFillet=true;
    //技能还可使用次数
    public int[] chances = new int[4] ;
    //以下为总次数
    public int eraseChances;
    public int teleportChances;
    public int breakdownChances;
    public int filletChances;
    //调用技能提示
    public Console0Script console;

    //复活按钮
    public GameObject restart;

    
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        line = GameObject.Find("teleportline").GetComponent<Transform>();
        erasepointer= GameObject.Find("erasesign").GetComponent<Transform>();

        //timers
        invtimer = invtime;
        erasetimer = erasetime;
        fillettimer = fillettime;

        //获取技能控制台
        console=Console0Script.instance;

        //技能可使用次数初始化
        chances[0] = eraseChances;
        chances[1] = teleportChances;
        chances[2] = breakdownChances;
        chances[3] = filletChances;

        //获取复活键
        restart = GameObject.FindGameObjectWithTag("Restart");

        
    }

    // Update is called once per frame
    void Update()
    {
        if(died)
        {
            return;
        }
        if(restart != null)
        {
            restart.SetActive(false);
        }
        
        //Debug.Log("mouse at:" + Input.mousePosition);
        Move();
      
        if (intheair)
        {
            ani.SetBool("InAir", true);
        }
        else
        {
            ani.SetBool("InAir", false);
        }

        //When killed
        if (HP <= 0)
        {
            Destroyed();
        }
        //When fall out of the world
        if (transform.position.y < -32.0f)
        {
            Destroyed();
        }
        if (invtimer > 0)
        {
            invtimer = invtimer - Time.deltaTime;
        }

        //Specs
        Teleport();

        if (erasetimer > 0)
        {
            erasetimer = erasetimer - Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                erasetimer = erasetime;
                if (canErase && chances[0] >0)//可使用时
                {
                    Erase();
                }

            }
        }
        if (fillettimer > 0)
        {
            fillettimer = fillettimer - Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                fillettimer = fillettime;
                if (canFillet && chances[3] >0)//可使用时
                {
                    Fillet();
                }
            }
        }
        if (canBreakdown && chances[2] >0)//可使用时
        {
            Breakdown();
        }
    }
    
    void Move()
    {
        RT = Input.GetAxis("Horizontal") * speed;
        
        //Walk
        if (RT >= 0)
        {
            transform.rotation = new Quaternion(0,0,0,0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        rigidbody2d.velocity = new Vector2(RT, rigidbody2d.velocity.y);
        if ((rigidbody2d.velocityX > 0.2f || rigidbody2d.velocityX < -0.2f)&&(canjump))
        {
            ani.SetBool("IsMove", true);
        }
        else
        {
            ani.SetBool("IsMove", false);
        }

        //jump
        if (canjump)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, speed*1.2f);
                canjumptwice = true;
            } 
        }
        else
        {
            if (canjumptwice)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, speed * 1.3f);
                    canjumptwice = false;
                }
            }
        }

    }
    
    void Teleport()
    {
        if (canTeleport && chances[1] >0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                rdyfortele = rdyfortele * -1;
            }
            if (rdyfortele == 1)
            {
                line.gameObject.SetActive(true);
            }
            else
            {
                line.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (rdyfortele == 1)
                {
                    Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Debug.Log("mouse,player,direction" + mousepos);
                    //transform.position = mousepos;
                    rigidbody2d.MovePosition(mousepos);
                    rdyfortele = -1;
                    chances[1]--;
                    console.Generate(1);
                }
            }
        }
        else
        {
            line.gameObject.SetActive(false);
        }
        
    }
    void Erase()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousepos.x - transform.position.x, mousepos.y - transform.position.y);
        direction.Normalize();
        GameObject eraseobject = Instantiate(erase, rigidbody2d.position + Vector2.up * 0.3f, Quaternion.identity);
        erase eraseproj = eraseobject.GetComponent<erase>();
        eraseproj.launch(direction);

        erasepointer.gameObject.SetActive(true);
        ani.SetTrigger("Erase");
        chances[0]--;
        console.Generate(0);
    }
    void Fillet()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousepos.x - transform.position.x, mousepos.y - transform.position.y);
        direction.Normalize();
        GameObject filletobject = Instantiate(fillet, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        fillet filletproj = filletobject.GetComponent<fillet>();
        filletproj.launch(direction);
        chances[3]--;
        console.Generate(3);
    }

    void Breakdown()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rdyforbreak = rdyforbreak * -1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rdyforbreak == 1)
            {
                GameObject detectobject=Instantiate(detect, rigidbody2d.position, Quaternion.identity);
                //Debug.Log("Breakdown!");
                rdyforbreak = -1;
                //chances[2]--;
                console.Generate(2);
            }
        }
    }
    public void ChangeHP(int value)
    {
        if (value < 0)
        {
            if (invtimer <= 0)
            {
                HP = HP + value;
                invtimer = invtime;

                ani.SetTrigger("Hurt");
                console.hurt = value;
                console.Generate(4);//受到伤害提示
            }
        }
        else
        {
            HP = HP + value;
        }
        
        //Debug.Log("HP is:" + HP);
    }

    public void Konckback(Vector2 direction,float force)
    {
        rigidbody2d.AddForce(direction * force);

    }
    public void Destroyed()
    {
        HP = 0;
        died = true;
        rigidbody2d.simulated = false;
        ani.SetTrigger("Killed");
        if(console?.tips!=null)//死亡时跳出复活按钮
        {
            foreach (GameObject tip in console.tips)
            {
                Destroy(tip);
            }
        }
        restart = RestartHappening.instance.restart;
        
        console.Generate(5);
        restart.SetActive(true);
    }
    public void TakedownSavepoint()
    {
        LogicScript.instance.startPos=transform.position;
    }
    public void Resqwan()
    {
        transform.position = LogicScript.instance.startPos;
    }
    
}
