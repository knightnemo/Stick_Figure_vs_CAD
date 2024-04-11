using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Bosscontroller : MonoBehaviour
{
    public static Bosscontroller instance {  get; private set; }
    public playercontroller player;
    
    public float lengthtimer;
    private float distance;
    int lor;
    public float speed = 2.0f;
    public GameObject bullet;

    
    public GameObject[] sounds;
    public AudioClip[] clips;
    public AudioSource aud;

    
    Animator ani;
    Rigidbody2D rb;
    SpriteRenderer rend;
    //attackchoose
    float attacktime = 8.0f;
    float attacktimer;
    int attacktype = 0;//type of normal shots 1:circle 2:direct 4:random F 3:laser 5:choose 
    //MultiShoot
    private int MultiTrack;//track type of MultiShoot 
    float multtime = 0.04f;
    float multtimer;
    int multnum = 100;
    int multcount = 0;
    //DirectShoot
    float directtime = 1f;
    float directtimer;
    int directnum = 6;
    int directcount = 0;
    public GameObject dart;
    //Laser
    float lasertime = 4f;
    float lasertimer;
    int lasernum = 1;
    int lasercount = 0;
    public GameObject beam;
    //random F
    float Ftime = 1.0f;
    float Ftimer;
    int Fnum = 2;
    int Fcount = 0;
    public GameObject flyspike;
    //Eliminate
    int energy = 0;
    bool canrelease = true;
    float elmtime = 20.0f;
    float elmtimer;
    public GameObject choose;
    public GameObject mark;
    public GameObject powermark;
    //Select
    float selecttime = 0.5f;
    float selecttimer;
    int selectnum = 2;
    int selectcount = 0;
    public GameObject selectline;
    //Blow
    float x0, y0;
    bool recorded=false;
    public float blowforce = 10f;
    float blowtime = 3.0f;
    float blowtimer;
    //HP
    public int HP = 1000;
    bool dead = false;
    float vanishtime = 3;
    float vanishtimer;
    Color texturecolor;
    float color=1.0f;
    //Jump
    public GameObject shock;
    public bool isfall = false;
    public bool canjump = false;
    public float heightlim = 75;
    //Walk
    float walktime = 1.0f;
    float walktimer;
    public GameObject teleeffect;

    public bool isMulti = false;
    public bool isDirect = false;
    public bool isLaser = false;
    public bool isF = false;
    public bool isBlow = false;
    public bool isJump = false;
    public bool iselm= false;
    public bool isselect=false;
    bool isWalk = false;
    public bool attacking=false;

    //change target
    public int targettype = 1;//1:Player 2:Empress of light
    Transform target;
    //siwtch
    public bool ON=true;
    // Start is called before the first frame update
    void Start()
    {
        
        attacktimer = attacktime;
        blowtimer = blowtime;
        multtimer = multtime;
        directtimer = directtime;
        lasertimer = lasertime;
        Ftimer = Ftime;
        elmtimer = elmtime;
        selecttimer = selecttime;
        vanishtimer = vanishtime;
        walktimer = walktime;
        targettype = 1;

        player = playercontroller.instance;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        aud=GetComponent<AudioSource>();

        player.LenSize = 14.0f;
    }
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (EOLcontroller.instance != null)
        {
            player.LenSize = 21.0f;
        }
        if (!ON || playercontroller.instance.atdev)
        {
            return;
        }
        //test
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            energy = 5;
        }
        if (HP <= 0)
        {
            dead = true;
        }
        if (dead)
        {
            Vanish();
            return;
        }
        if (targettype == 1)
        {
            target=player.transform;
            attacktime = 8;
        }
        if(targettype == 2)
        {
            target = EOLcontroller.instance.transform;
            attacktime = 5;
        }
        distance=target.position.x-transform.position.x;
        if (distance>0) lor = 1;//1:target at right -1:at left
        else
        {
            lor = -1;
        }

        //Move
        if (Mathf.Abs(distance) > 3f && Mathf.Abs(distance) < 50f && !attacking && !isJump)
        {
            Move();
        }
        else
        {
            ani.SetBool("Walk", false);
        }

        //jump
        if (transform.position.y + 16 > heightlim)
        {
            canjump = true;
        }
        else
        {
            canjump = false;
        }
        if ((Mathf.Abs(distance) >= 50f && !attacking)||(target.transform.position.y - transform.position.y-16 > 30 && !attacking) )
        {
            if (canjump && target.position.y>heightlim-3)
            {
                isJump = true;
                attacking = true;
            }
            else
            {
                if (ON)
                {
                    transform.position = target.position - Vector3.up * 16 - Vector3.right * 5;
                    Instantiate(teleeffect, transform.position+ Vector3.up * 16, Quaternion.identity);
                }
                
            }
        }
        if (isJump)
        {
            Jump();
        }
        else
        {
            ani.SetBool("Jump", false);
            ani.SetBool("Fall", false);
            
        }
        //blow
        if (Mathf.Abs(distance) < 5f &&!attacking && Mathf.Abs(target.position.y - transform.position.y-16)<5f)
        {
            isBlow = true;
            attacking = true;
        }
        //Eliminate
        if (energy >= 5 && !attacking)
        {
            energy = 0;
            attacking = true;
            iselm = true;
        }
        //select attack
        if (!attacking)
        {
            if (attacktimer > 0)
            {
                attacktimer -= Time.deltaTime;
            }
            else
            {
                attacktype= Random.Range(1, 6);
                if (attacktype == 1)
                {
                    MakeSound(4);
                    isMulti = true;
                    attacking = true;
                }
                if(attacktype == 2)
                {
                    isDirect = true;
                    attacking = true;
                }
                if (attacktype == 3)
                {
                    MakeSound(3);
                    isLaser = true;
                    attacking = true;
                }
                if(attacktype == 4)
                {
                    MakeSound(5);
                    isF = true;
                    attacking = true;
                }
                if( attacktype == 5)
                {
                    MakeSound(8);
                    isselect = true;
                    attacking= true;
                }
                attacktype = 0;
                attacktimer = attacktime;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.M))
        {
            isMulti = true;
            attacking = true;
            lengthtimer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            isBlow = true;
            attacking = true;
        }
        if (Mathf.Abs(distance) <= 10f&&!isJump)
        {
            //isMulti = true;
            //lengthtimer = 0f;
        }
        

        

        if (isMulti)
        {
            //MakeSound(0);
            //StartCoroutine(Wait());
            MultiShoot();
            
        }
        if (isDirect)
        {
            //MakeSound(1);
            //StartCoroutine(Wait()); ;
            Directshoot();
        }
        if (isLaser)
        {
            //MakeSound(2);
            //StartCoroutine(Wait());
            Laser();
        }
        if (isF)
        {
            //MakeSound(5);
            //StartCoroutine(Wait());
            RandomF();
        }
        if (iselm)
        {
            //MakeSound(4);
            //StartCoroutine(Wait());
            Eliminate();
        }
        if (isselect)
        {
            //MakeSound(5);
            //StartCoroutine(Wait());
            Select();
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

        
    }
    void Move()
    {
        if(walktimer > 0)
        {
            walktimer-= Time.deltaTime;
        }
        else
        {
            MakeSound(9);
            walktimer = walktime;
        }
        
        Vector2 pos=transform.position;
        pos.x += lor * speed * Time.deltaTime;
        transform.position = pos;
        ani.SetBool("Walk", true);
    }

    void Blow()
    {
        MakeSound(1);
        if(lor==1)
        {
            ani.SetBool("BlowRight", true);
            if(transform.position.x<x0+10f)
            {
                transform.Translate(30f*Vector3.right*Time.deltaTime);
            }
        }
        if(lor==-1)
        {
            ani.SetBool("BlowLeft", true);
            if (transform.position.x > x0 - 10f)
            {
                transform.Translate(30f*Vector3.left * Time.deltaTime);
            }
        }
        if (blowtimer > 0)
        {
            blowtimer -= Time.deltaTime;
        }
        else
        {
            attacking = false;
            isBlow = false;
            blowtimer = blowtime;
        }
    }

    void MultiShoot()
    {
        
        ani.SetBool("MultiShoot",true);
        if (multcount < multnum)
        {
           
            if (multtimer > 0)
            {
                multtimer -= Time.deltaTime;
            }
            else
            {
                GameObject temp = Instantiate(bullet, transform.position+Vector3.up*16 , transform.rotation);
                Circle circle = temp.GetComponent<Circle>();
                circle.tracktype = MultiTrack++;
                multcount++;

                multtimer = multtime;
            }
        }
        else
        {
            multcount = 0;
            isMulti = false;
            attacking = false;
            ani.SetBool("MultiShoot", false);
        }
                     
    }

    void Directshoot()
    {
        
        if (lor == 1)
        {
            ani.SetTrigger("ShootRight");
        }
        if (lor == -1)
        {
            ani.SetTrigger("ShootLeft");
        }

        if (directcount < directnum)
        {
            if(directtimer > 0)
            {
                directtimer-= Time.deltaTime;
            }
            else
            {
                MakeSound(10);
                directtimer = directtime;
                Vector2 tmp=target.position-transform.position- Vector3.up * 16;
                float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
                //angle += 60;
                for(int i = -4; i <= 4; i++)
                {
                    GameObject temp = Instantiate(dart, transform.position + new Vector3(lor*3f+4*Mathf.Cos((angle+15.0f*i)*Mathf.Deg2Rad),16+  4*Mathf.Sin((angle + 15.0f * i) * Mathf.Deg2Rad), 0f), Quaternion.AngleAxis(angle+15.0f*i, Vector3.forward));
                    directshot proj = temp.GetComponent<directshot>();
                    proj.deg = angle + 15.0f * i;
                }
                directcount++;
            }
        }
        else
        {
            directcount = 0;
            isDirect = false;
            attacking = false;
        }
    }

    void Laser()
    {
        
        if (lasertimer > 0)
        {
            lasertimer -= Time.deltaTime;
            ani.SetBool("Laser", true);
            if (lasercount < lasernum)
            {
                Vector2 tmp = target.position - transform.position - Vector3.up * 25;
                float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg-60;
                if (targettype == 2)
                {
                    angle += 60;
                }
                Instantiate(beam, transform.position+ 25*Vector3.up, Quaternion.AngleAxis(angle , Vector3.forward));
                lasercount++;
            }
        }
        else
        {
            lasercount = 0;
            isLaser = false;
            attacking= false;

            ani.SetBool("Laser", false);
            lasertimer = lasertime;
        }
    }

    void RandomF()
    {
        
        if (Fcount < Fnum)
        {
            if (Ftimer > 0)
            {
                Ftimer -= Time.deltaTime;
            }
            else
            {
                GameObject temp=Instantiate(flyspike, target.transform.position + 10 * Vector3.up, Quaternion.identity);
                flyspike proj = temp.GetComponent<flyspike>();
                proj.xc = target.position.x; 
                proj.yc = target.position.y;
                Fcount++;
                Ftimer = Ftime;
            }
        }
        else
        {
            Fcount = 0;
            isF = false;
            attacking = false;

        }
    }

    void Eliminate()
    {
        if (canrelease)
        {
            MakeSound(6);
            Instantiate(choose,transform.position + 16 * Vector3.up, Quaternion.identity);
            Instantiate(mark, transform.position+16*Vector3.up, Quaternion.identity);
            canrelease = false;
        }
        if (elmtimer > 0)
        {
            player.LenSize = 25.0f;
            elmtimer -= Time.deltaTime;
            ani.SetBool("Eliminate", true);
        }
        else
        {
            elmtimer = elmtime;
            player.LenSize = 14.0f;
            canrelease = true;
            iselm = false;
            attacking = false;
            ani.SetBool("Eliminate", false) ;
        }
    }
    
    void Select()
    {
        if (lor == 1)
        {
            ani.SetTrigger("ShootRight");
        }
        if (lor == -1)
        {
            ani.SetTrigger("ShootLeft");
        }

        if (selectcount < selectnum)
        {
            if (selecttimer > 0)
            {
                selecttimer -= Time.deltaTime;
            }
            else
            {
                
                Vector2 tmp = target.position - transform.position - Vector3.up * 17-Vector3.right*lor*5;
                float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
                Instantiate(selectline, transform.position + Vector3.up * 17 + Vector3.right * lor * 5, Quaternion.AngleAxis(angle, Vector3.forward));
                selecttimer = selecttime;
                selectcount++;
            }
        }
        else
        {
            selectcount = 0;
            isselect = false;
            attacking= false;
        }
    }

    void Jump()
    {
        player.LenSize = 30.0f;
        if(transform.position.y-player.transform.position.y<20f&&!isfall)
        {
            rb.velocityY = 2f;
            transform.position += Vector3.up * 40f*Time.deltaTime;
        }
        else
        {
            
            if(Mathf.Abs(transform.position.x-player.transform.position.x)>5f)
                transform.Translate(20f*Vector3.right*lor*Time.deltaTime);
            else
            {
                rb.velocityY = -2f;
                transform.position -= Vector3.up * 40f * Time.deltaTime;
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
            isfall = true;
            ani.SetBool("Jump", false);
            ani.SetBool("Fall", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isJump)
        {
            if (collision.gameObject.tag == "Player" && isfall)
            {
                player.transform.position += 0.1f * Vector3.up;
                player.rigidbody2d.AddForce(10*Vector2.up);//»÷·ÉPlayer
                player.ChangeHP(-1);

                isJump = false;
                isfall = false;
                Instantiate(shock, transform.position + Vector3.up * 11.5f, Quaternion.identity);
                attacking = false;
                player.LenSize = 14.0f;

            }
            if((collision.gameObject.tag == "ground" || collision.gameObject.tag=="barrier") && isfall && (transform.position.y+16 - player.transform.position.y)<16)
            {
                isJump= false;
                isfall = false;
                Instantiate(shock,transform.position+Vector3.up*11.5f,Quaternion.identity);
                attacking = false;
                player.LenSize = 14.0f;
            }
            if ((collision.gameObject.tag == "ground" || collision.gameObject.tag == "barrier") && isfall && transform.position.y + 16 - player.transform.position.y > 10)
            {
                rb.position -= Vector2.up*7;
            }
            if ((collision.gameObject.tag == "ground" || collision.gameObject.tag == "barrier") && rb.velocityY>0)
            {
                
                rb.position += Vector2.up * 20;
            }
        }
        if(isBlow)
        {
            if(collision.gameObject.tag=="Player")
            {
                Debug.Log("player is hit!");
                player.transform.position += Vector3.up * 0.1f;
                player.rigidbody2d.AddForce(new Vector2(lor, 1)*blowforce);//»÷·ÉPlayer
                isBlow = false;
                attacking = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "erase")
        {
            energy++;
            HP--;
            Instantiate(powermark, transform.position + Vector3.up * 16, Quaternion.identity);
        }
    }

    void Vanish()
    {
        if (vanishtimer > 0)
        {
            vanishtimer -= Time.deltaTime;
            color += 0.002f;
            texturecolor = new Color(color * 1.1f, color, color);
            rend.material.color= texturecolor;
        }
        else
        {
            vanishtimer = vanishtime;
            
            playercontroller.instance.rdy2 = true;
            playercontroller.instance.barriernum = 5;
            Destroy(gameObject);
        }
    }
    
    public void MakeSound(int n)
    {
        //sounds[n].GetComponent<AudioSource>().Play();
        aud.clip = clips[n];
        aud.Play();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
    }

}
