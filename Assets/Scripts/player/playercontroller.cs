using JetBrains.Annotations;
using NUnit.Framework;
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

    public Rigidbody2D rigidbody2d;
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
    //Boss
    public bool EliminateSafe = false;
    public bool Selected = false;
    public float Selectedtime = 5.0f;
    float Selectedtimer;
    public int barriernum = 1;
    //End
    public bool rdy1 = false;
    public bool rdy2 = false;
    public bool cankillall= false;
    public bool killall=false;
    float killTime = 0.2f;
    float killTimer;
    int killnum = 30;
    int killcount = 0;
    float offset = 1.5f;
    public GameObject killbeam;
    public bool End = false;
    public bool rdytoconc = false;
    public int type = 1;
    public bool atdev = false;
    //Genshin
    public bool rdyforstart = false;
    public bool candetonate = false;
    float detonatetime = 10.0f;
    float detonatetimer;
    //Camera
    public float LenSize=8.0f;

    public GameObject[] sounds;
    public Bosscontroller boss;
    public GameObject[] bosssounds;
    public AudioClip[] clips;
    public AudioSource aud;

    public int score=0;
    void Awake()
    {
        isUpsideDown=false;
        instance = this;
        rightclick=false;
        leftclick=false;
        cooldown=false;
        cooldownTimer=cooldownTime;
        LostAttack=false;
        rdy1 = false;
        rdy2 = false;
        cankillall = false;
        killall = false;
        upsidedowntimer=upsidedowntime;
        type = 1;
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
        detonatetimer = detonatetime;
        Selectedtimer = Selectedtime;
        killTimer = killTime;

        //��ȡ���ܿ���̨
        console =Console0Script.instance;

        //���ܿ�ʹ�ô�����ʼ��
        chances[0] = 0;//erase
        chances[1] = 0;//teleport
        chances[2] = 0;//breakdown
        chances[3] = filletChances;//fillet

        //��ȡ�����
        restart = GameObject.FindGameObjectWithTag("Restart");
        //Boss
        EliminateSafe = false;
        Selected = false;

        boss=Bosscontroller.instance;
        aud=GetComponent<AudioSource>();

        score = LogicScript.instance.lastScore;
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
        if (Selected)
        {
            if (Selectedtimer > 0)
            {
                Selectedtimer -= Time.deltaTime;
            }
            else
            {
                Selectedtimer = Selectedtime;
                Selected = false;
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
        if (End)
        {
            rigidbody2d.simulated = false;
            return;
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
        if (killall)
        {
            Killall();
        } 
        Genshinstart();

        if(boss!=null)
        {
            if (boss.isMulti)
            {
                BossSound(0);
            }
            if (boss.isDirect)
            {
                BossSound(1);
            }
            if (boss.isLaser)
            {
                BossSound(2);
            }
            if (boss.isF)
            {
                BossSound(3);
            }
            if (boss.iselm)
            {
                BossSound(4);
            }
            if (boss.isselect)
            {
                BossSound(5);
            }
            if(boss.isBlow)
            {
                BossSound(6);
            }
        }
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
                MakeSound(1);
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
                    MakeSound(1);
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
                MakeSound(3);
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
                    MakeSound(2);
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
        MakeSound(0);
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousepos.x - transform.position.x, mousepos.y - transform.position.y);
        direction.Normalize();
        if (cankillall)
        {
            direction = Vector2.up;
            MakeSound(12);
        }
        GameObject eraseobject = Instantiate(erase, rigidbody2d.position + Vector2.up * 0.3f, Quaternion.identity);
        erase eraseproj = eraseobject.GetComponent<erase>();
        eraseproj.launch(direction);

        erasepointer.gameObject.SetActive(true);
        ani.SetTrigger("Erase");
        chances[0]--;
        console.Generate(0);
        //cankillall = false;
    }
    void Fillet()
    {
        MakeSound(4);
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
            MakeSound(3);
            rdyforbreak = rdyforbreak * -1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rdyforbreak == 1)
            {
                MakeSound(5);
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
                    MakeSound(7);
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
                if (Selected)
                {
                    HP = HP + value*3;
                }
                else
                {
                    HP = HP + value;
                }
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
        MakeSound(10);
        //ani.SetTrigger("Killed");
        ChangeAni(2);
        LogicScript.instance.deathNum++;
        LogicScript.instance.lastScore = score;
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
        LogicScript.instance.lastScore = LogicScript.instance.finalScore;
    }
    public void Resqwan()
    {
        if (LogicScript.instance != null)
        {
            transform.position = LogicScript.instance.startPos;

        }
        
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

    void Killall()
    {
        if (killcount < killnum)
        {
            if (killTimer > 0)
            {
                killTimer-= Time.deltaTime;
            }
            else
            {
                MakeSound(13);
                killTimer = killTime;
                Instantiate(killbeam,transform.position+offset*Vector3.right, Quaternion.identity);
                Instantiate(killbeam, transform.position - offset * Vector3.right, Quaternion.identity);
                offset += 13.0f;
                killcount++;
            }
            if (killcount == 5)
            {
                //Bosscontroller.instance.HP = 0;
            }
        }
        else
        {
            killcount = 0;
            killall = false;
        }
    }
    void Genshinstart()
    {
        if (rdyforstart)
        {
            if (detonatetimer > 0)
            {
                detonatetimer -= Time.deltaTime;
            }
            else
            {
                candetonate = true;
            }
        }
    }

    public void MakeSound(int n)
    {
        //if (sounds != null)
        // if (sounds[n]!=null)
        //sounds[n].GetComponent<AudioSource>().Play();
        if(clips==null)
        {
            Debug.Log("clips==null");
            return;
        }
        if(clips.Length>n)
        {
            
            aud.clip = clips[n];
            aud.Play();
        }
        
    }
    void BossSound(int n)
    {
        bosssounds[n].GetComponent<AudioSource>().Play();
    }
}
