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
    public bool isUpsideDown;
    public float upsidedowntime;
    float upsidedowntimer;

    Rigidbody2D rigidbody2d;
    public float speed = 5.0f;
    public float jumpspeed = 6.0f;
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
    //Shield
    public int shieldnum = 3;
    bool shieldopen = false;
    Transform Shield;

    //Animation
    Animator ani;
    public GameObject player0;
    public GameObject Died;
    public GameObject Hurt;
    float timer;
    public float hurttime = 1.0f;
    bool timeup=false;

    //�����ܷ�ʹ�ã���ʼ��Ϊfalse
    public bool canErase=false;
    public bool canTeleport=true;
    public bool canBreakdown=true;
    public bool canFillet=true;
    //���ܻ���ʹ�ô���
    public int[] chances = new int[4] ;
    //����Ϊ�ܴ���
    public int eraseChances;
    public int teleportChances;
    public int breakdownChances;
    public int filletChances;
    //���ü�����ʾ
    public Console0Script console;

    //���ť
    public GameObject restart;
    //change-speed related
    public float keyDetectTime;
    public float cooldownTime;
    float detectTimer;
    float cooldownTimer;
    bool rightclick,leftclick,cooldown;
    public float chargespeed;
    //lost attack related
    public bool LostAttack; 
    public float LostAttackTime;
    public float LAtimer;
    
    void Awake()
    {
        isUpsideDown=false;
        instance = this;
        rightclick=false;
        leftclick=false;
        cooldown=false;
        cooldownTimer=cooldownTime;
        LostAttack=false;
        upsidedowntimer=upsidedowntime;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Stand at 0,0;
        
        detectTimer=keyDetectTime;
        rigidbody2d = GetComponent<Rigidbody2D>();
        ani = player0.GetComponent<Animator>();

        //All things attached to player
        line = GameObject.Find("teleportline").GetComponent<Transform>();
        erasepointer= GameObject.Find("erasesign").GetComponent<Transform>();
        Shield = GameObject.FindGameObjectWithTag("shield").GetComponent<Transform>();

        //timers
        invtimer = invtime;
        erasetimer = erasetime;
        fillettimer = fillettime;

        //��ȡ���ܿ���̨
        console=Console0Script.instance;

        //���ܿ�ʹ�ô�����ʼ��
        chances[0] = eraseChances;
        chances[1] = teleportChances;
        chances[2] = breakdownChances;
        chances[3] = filletChances;

        //��ȡ�����
        restart = GameObject.FindGameObjectWithTag("Restart");

        
    }

    // Update is called once per frame
    void Update()
    {   
        if(isUpsideDown){
            upsidedowntimer-=Time.deltaTime;
            if(upsidedowntimer<=0){
                isUpsideDown=false;
                Vector3 currentScale = transform.localScale;
                currentScale.y *= -1f; // 反转Y轴的缩放
                transform.localScale = currentScale;
                upsidedowntimer=upsidedowntime;
                Debug.Log("y轴恢复正常");
            }
        }
        //Debug.Log(speed);
        if(Input.GetKeyDown(KeyCode.D)){
                    rightclick=true;
        }
        if(Input.GetKeyDown(KeyCode.A)){
                    leftclick=true;
        }
        if(rightclick||leftclick){
        if(detectTimer>=0){
            detectTimer-=Time.deltaTime;
            if((keyDetectTime-detectTimer)>0.1f){
            if(rightclick){
                if(Input.GetKeyDown(KeyCode.D)){
                    speed=chargespeed;
                    cooldown=true;
                }
            }
            if(leftclick){
                if(Input.GetKeyDown(KeyCode.A)){
                    speed=chargespeed;
                    cooldown=true;
                }
            }}
        }
        else{
            detectTimer=keyDetectTime;
            leftclick=false;
            rightclick=false;
        }
        }
        if(cooldown){
            cooldownTimer-=Time.deltaTime;
            if(cooldownTimer<0){
                speed=5.0f;
                cooldown=false;
                cooldownTimer=cooldownTime;
            }
        }
        //以上实现加速跑
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
      
        if (intheair&&rigidbody2d.velocityY>0)
        {
            ani.SetInteger("Air", 1);
        }
        else if(intheair&&rigidbody2d.velocityY<0)
        {
            ani.SetInteger("Air", -1);
        }
        else if(!intheair)
        {
            ani.SetInteger("Air", 0);
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
                if (canErase && chances[0] >0)//��ʹ��ʱ
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
                if (canFillet && chances[3] >0)//��ʹ��ʱ
                {
                    Fillet();
                }
            }
        }
        if (canBreakdown && chances[2] >0)//��ʹ��ʱ
        {
            Breakdown();
        }
        //Shield
        Defend();
        //lost attack state:
        if (LostAttack){
            LAtimer-=Time.deltaTime;
            if(LAtimer<0){
            Debug.Log("Player Can Attack again");
            LostAttack=false;
            canErase=true;
            canTeleport=true;
            canBreakdown=true;
            canFillet=true;//FIXME:这一块留了个小坑，就是可能被:wq击中时候还有一些技能没有解锁
            //它出现时四个技能已经获取完了，因此不用考虑这个问题
            }
        }

        if (timeup) CountDown();
    }
    
    void Move()
    {
        RT = Input.GetAxis("Horizontal") * speed;
        
        //Walk
        if (RT > 0)
        {
            transform.rotation = new Quaternion(0,0,0,0);
        }
        else if(RT < 0)
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
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpspeed*1.2f);
                canjumptwice = true;
            } 
        }
        else
        {
            if (canjumptwice)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpspeed * 1.3f);
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
    //Use shield
    void Defend()
    {
        if (shieldnum > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (shieldopen == false)
                {
                    shieldopen = true;
                    Debug.Log("open the shield!");
                    ani.SetTrigger("Openshield");
                }
                else
                {
                    shieldopen = false;
                }

            }
        }
        else
        {
            shieldopen = false;
        }

        if (shieldopen)
        {
            Shield.gameObject.SetActive(true);
        }
        else
        {
            Shield.gameObject.SetActive(false);
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

                //ani.SetTrigger("Hurt");
                ChangeAni(1);
                timeup = true;
                console.hurt = value;
                console.Generate(4);//�ܵ��˺���ʾ
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
        //ani.SetTrigger("Killed");
        ChangeAni(2);
        if(console?.tips!=null)//����ʱ�������ť
        {
            foreach (GameObject tip in console.tips)
            {
                Destroy(tip);
            }
        }
        restart = RestartHappening.instance.restart;
        if (restart != null) Debug.Log("not null");
        else Debug.Log("NULL!");
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
    void ChangeAni(int n)
    {
        if(n==1)
        {
            player0.SetActive(false);
            Died.SetActive(false);
            Hurt.SetActive(true);
        }
        if (n == 0)
        {
            player0.SetActive(true);
            Died.SetActive(false);
            Hurt.SetActive(false);
            return;
        }
        if (n==2)
        {
            player0.SetActive(false);
            Died.SetActive(true);
            Hurt.SetActive(false);
            return;
        }
    }
    void CountDown()
    {
        if (timer < hurttime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            timeup = false;
            ChangeAni(0);
        }
    }
}
