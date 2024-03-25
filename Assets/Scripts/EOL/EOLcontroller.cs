using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;

public class EOLcontroller : MonoBehaviour
{
    public static EOLcontroller instance { get; private set; }

    public Transform target;

    //Movement
    public bool rdyfordash = false;
    Vector2 destination;
    float stayrange = 8.0f;
    float speed = 4.0f;
    float shifttime = 3.0f;
    float shifttimer;
    int shifttype = 1;
    //attack basic
    bool attacking = false;
    public float attacktime = 5.0f;
    float attacktimer;
    int attacktype=0;//0 means no attack,when finish attacking,turn this back to 0.
    //spear arrays
    int arrayrounds = 0;
    float arraytime = 3.0f;
    float arraytimer;
    public int spearmode = 0;//1:y 2:x 3:45 4:-45 5:aim at target
    public GameObject lightspear;
    public GameObject aimline;
    //aim spears
    float angle = 0.0f;
    float aimtime = 0.2f;
    float aimtimer;
    int aimrounds = 0;
    //crystal
    public GameObject lightcrystal;
    float crystalfiretime = 0.4f;
    float crystalfiretimer;
    int crystalrounds = 0;
    //dash
    Vector2 dashdirection;
    float tmpx;
    float tmpy;
    public bool start=true;
    public bool preparing = false;
    public bool dashing = false;
    public bool finish = false;
    float dashtime = 2.5f;
    float dashtimer;
    float height;
    //color the spears
    public int spearcolor = 1;

    //HP(only use when fighting whth boss)
    public int HP = 500;
    bool killed = false;
    float fadetime = 2.0f;
    float fadetimer;
    Renderer rend;
    Color texturecolor;
    float colortime = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        //Get target.When PKUman is added,change the tag.
        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            target = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        }
        
        //timers
        attacktimer = attacktime;
        arraytimer = arraytime;
        aimtimer = aimtime;
        crystalfiretimer = crystalfiretime;
        dashtimer = dashtime;
        fadetimer = fadetime;
        shifttimer = shifttime;

        rend=GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            return;
        }
        if (killed)
        {
            Fade();
            return;
        }

        //This is a test button,delete after finish the final boss.
        if (Input.GetKeyDown(KeyCode.K))
        {
            HP = 0;
        }

        Move();

        if (HP <= 0)
        {
            killed = true;
            HP = 0;
        }
        if (!attacking)
        {
            if (attacktimer > 0)
            {
                attacktimer -= Time.deltaTime;
            }
            else
            {
                attacktype = Random.Range(1, 5);
                Debug.Log("attack in type " + attacktype);
                attacktimer = attacktime;
                attacking = true;
            }
        }

        if (attacktype == 1)
        {
            Speararray();
            Debug.Log("Deploy spear arrays!");
        }
        if(attacktype == 2)
        {
            Aimspear();
            Debug.Log("Aim at target!");
        }
        if(attacktype == 3)
        {
            Crystal();
            Debug.Log("Fire crystals!");
        }
        if (attacktype == 4)
        {
            Debug.Log("Dash at target!");
            rdyfordash = true;
            Dash();
        }
    }

    void Move()
    {
        if (shifttimer > 0)
        {
            shifttimer -= Time.deltaTime;
        }
        else
        {
            shifttimer = shifttime;
            shifttype++;
            if (shifttype > 3)
            {
                shifttype = 1;
            }
        }
        destination=target.position;
        if(shifttype == 1)
        {
            destination.y += 25.0f;
            destination.x += 30.0f;
        }
        if (shifttype == 2)
        {
            destination.y += 25.0f;
            destination.x -= 30.0f;
        }
        if(shifttype == 3)
        {
            destination.y += 40.0f;
            
        }
        
        Vector2 distance=new Vector2(destination.x-transform.position.x,destination.y-transform.position.y);
        Vector2 movedirection = distance.normalized;

        Vector2 pos = transform.position;
        if (!rdyfordash)
        {
            if(distance.magnitude > stayrange)
            {
                pos=pos+movedirection*speed*Time.deltaTime;
            }
            else
            {
                pos = pos + movedirection * speed * Time.deltaTime*0.3f;
            }
        }
        transform.position = pos;
        
    }

    void Speararray()
    {
        if (arraytimer > 0)
        {
            arraytimer -= Time.deltaTime;
        }
        if(arraytimer < 0)
        {
            arrayrounds++;
            spearmode++;
            Debug.Log("Fire spears, round" + arrayrounds + "at mode" + spearmode);
            for(int i = -7; i <= 7; i++)
            {
                if (spearmode == 2)
                {
                    GameObject aimlineobj = Instantiate(aimline, new Vector2(transform.position.x , transform.position.y + 6 * i), Quaternion.identity);
                    GameObject spearobj = Instantiate(lightspear, new Vector2(transform.position.x - 15, transform.position.y + 6 * i), Quaternion.identity);
                }
                else
                {
                    GameObject aimlineobj = Instantiate(aimline, new Vector2(transform.position.x + 6 * i, transform.position.y), Quaternion.identity);
                    if (spearmode == 3)
                    {
                        GameObject spearobj = Instantiate(lightspear, new Vector2(transform.position.x + 6 * i-15, transform.position.y + 15), Quaternion.identity);
                    }
                    else
                    {
                        GameObject spearobj = Instantiate(lightspear, new Vector2(transform.position.x + 6 * i, transform.position.y + 15), Quaternion.identity);
                    }
                }

                
                if (spearcolor <=7)
                {
                    spearcolor++;
                }
                if (spearcolor > 7)
                {
                    spearcolor = 1;
                }
            }
            arraytimer = arraytime;
        }
        if (arrayrounds >= 4)
        {
            Debug.Log("Spear array done");
            arrayrounds = 0;
            spearmode = 0;
            attacktype = 0;
            attacking = false;
        }
    }

    void Aimspear()
    {
        spearmode = 5;
        if (aimtimer > 0)
        {
            aimtimer -= Time.deltaTime;
        }
        else
        {
            if (aimrounds <= 20)
            {
                aimrounds++;
                angle = angle + 30.0f;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Debug.Log("angle is " + angle+"Cos:"+ Mathf.Cos(angle*Mathf.Deg2Rad) +"Sin: "+ Mathf.Sin(angle * Mathf.Deg2Rad));
                GameObject aimlineobj = Instantiate(aimline, target.transform.position+Vector3.up*16, rotation);
                GameObject spearobj = Instantiate(lightspear, new Vector2(target.transform.position.x-15*Mathf.Sin(angle*Mathf.Deg2Rad),target.transform.position.y+15*Mathf.Cos(angle*Mathf.Deg2Rad)+16), rotation);
            }
            else if(aimrounds == 21)
            {
                aimrounds++;
            }

            aimtimer = aimtime;

            if (spearcolor <= 7)
            {
                spearcolor++;
            }
            if (spearcolor > 7)
            {
                spearcolor = 1;
            }
        }

        if(aimrounds > 21) 
        {
            aimrounds = 0;
            spearmode = 0;
            attacktype = 0;
            attacking = false;
        }

    }

    void Crystal()
    {
        if (crystalfiretimer > 0)
        {
            crystalfiretimer -= Time.deltaTime;
        }
        else
        {
            if (crystalrounds <=15)
            {
                crystalrounds++;
                GameObject crystalobj=Instantiate(lightcrystal,transform.position,Quaternion.identity);
            }
            crystalfiretimer = crystalfiretime;
            

        }
        if (crystalrounds > 15)
        {
            crystalrounds = 0;
            attacktype = 0;
            attacking = false;
        }
    }

    void Dash()
    {
        if (rdyfordash)
        {
            if (start)
            {
                preparing = true;
                start = false;
            }
        }
        
        if (preparing)
        {
            dashdirection=(target.position-transform.position).normalized;
            tmpx = dashdirection.x;
            tmpy= dashdirection.y;
            height = transform.position.y;
            dashing = true;
            preparing = false;
        }
        if (dashing)
        {
            if(dashtimer > 0)
            {
                dashtimer -= Time.deltaTime;
                Vector2 pos=transform.position;
                pos = pos + new Vector2(tmpx,tmpy) * 0.1f;
                transform.position = pos;
            }
            else
            {
                dashing = false;
                finish = true;
                tmpy = tmpy * -3;
                dashtimer = dashtime;
            }
        }
        if (finish)
        {
            Vector2 pos1=transform.position;
            pos1 = pos1 + new Vector2(tmpx, tmpy) * 0.1f;
            transform.position = pos1;
            if(transform.position.y-height>=-1 && transform.position.y - height <= 1)
            {
                finish = false;
                rdyfordash = false;
                attacktype = 0;
                attacking = false;
                start = true;
            }
        }
    }

    //Please change "player"into "boss"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            Bosscontroller.instance.HP-=50;
            
        }
    }

    void Fade()
    {
        if (fadetimer > 0)
        {
            fadetimer -= Time.deltaTime;
            colortime += 0.02f;
            texturecolor=new Color(colortime, colortime, colortime);
            rend.material.color = texturecolor;
        }
        else
        {
            fadetimer = fadetime;
            Bosscontroller.instance.targettype = 1;
            Debug.Log("Revenge for me, T......");
            Destroy(gameObject);
        }
    }
}
