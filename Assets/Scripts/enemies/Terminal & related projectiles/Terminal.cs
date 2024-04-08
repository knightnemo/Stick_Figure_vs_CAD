using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{   
    Transform terminalTransform;
    public GameObject rm;
    public GameObject wq;
    public GameObject beam;
    public GameObject rm_f;
    public GameObject tac;
    public GameObject help;
    public GameObject move;
    float force=300.0f;
    bool activestate=false;
    public GameObject ply;
    public float activedistance;
    public float interval=2.0f;
    
    Vector2 direction;
    float intervaltime;
    Transform targetTransform;
    Rigidbody2D rigidbody2d;
    public GameObject rmobject;
    bool beamstate;
    public float beamtime;
    
    float beamtimer;

    
    public float tmpx, tmpy;
    SpriteRenderer rend;

    public int HP = 5;
    public GameObject hurteffect;
    public GameObject deatheffect;

    public AudioClip[] clips;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        terminalTransform = GetComponent<Transform>();
        targetTransform = ply.transform;
        intervaltime=interval;
        beamstate=false;
        beamtimer=beamtime;

        rend=GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.1f, 1.1f, 1.1f);
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {//FIXME:预留接口，如果有一二阶段切换可以通过调整randomNumber的范围和intervaltime的数值实现
        if (HP <= 0)
        {
            playercontroller.instance.LenSize = 8.0f;
            Instantiate(deatheffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
            playercontroller.instance.LenSize = 8;
        }
        if(activestate){
            if(!beamstate){
                intervaltime-=Time.deltaTime;
                if(intervaltime<=0){
                    intervaltime=interval;
                    int randomNumber =Random.Range(1, 13);
                    if(randomNumber<=2){
                    rmobject = Instantiate(rm, (Vector2)terminalTransform.position, Quaternion.identity);
                    rigidbody2d =rm.GetComponent<Rigidbody2D>();
                    direction.x=targetTransform.position.x-terminalTransform.position.x;
                    direction.y=targetTransform.position.y-terminalTransform.position.y;
                    rigidbody2d.AddForce(-direction * force);//rm
                        MakeSound(2);
                    }
                    else if(randomNumber<=4){
                        beam_action();//rm-f
                    }
                    else if(randomNumber<=6){
                        rmobject = Instantiate(help, (Vector2)terminalTransform.position, Quaternion.identity);
                        rigidbody2d =help.GetComponent<Rigidbody2D>();
                        direction.x=targetTransform.position.x-terminalTransform.position.x;
                        direction.y=targetTransform.position.y-terminalTransform.position.y;
                        rigidbody2d.AddForce(-direction * force);//--help
                        MakeSound(2);
                    }
                    else if(randomNumber<=8){
                        rmobject = Instantiate(tac, (Vector2)terminalTransform.position, Quaternion.identity);
                        rigidbody2d =tac.GetComponent<Rigidbody2D>();
                        direction.x=targetTransform.position.x-terminalTransform.position.x;
                        direction.y=targetTransform.position.y-terminalTransform.position.y;
                        rigidbody2d.AddForce(-direction * force);//tac
                        MakeSound(2);
                    }
                    else if(randomNumber<=10){
                    rmobject = Instantiate(wq, (Vector2)terminalTransform.position, Quaternion.identity);
                    rigidbody2d =wq.GetComponent<Rigidbody2D>();
                    direction.x=targetTransform.position.x-terminalTransform.position.x;
                    direction.y=targetTransform.position.y-terminalTransform.position.y;
                    rigidbody2d.AddForce(-direction * force);//wq
                        MakeSound(2);
                    }
                    else{
                        rmobject = Instantiate(move, (Vector2)terminalTransform.position, Quaternion.identity);
                        rigidbody2d =move.GetComponent<Rigidbody2D>();
                        direction.x=targetTransform.position.x-terminalTransform.position.x;
                        direction.y=targetTransform.position.y-terminalTransform.position.y;
                        rigidbody2d.AddForce(-direction * force);//move
                        MakeSound(2);
                    }//随机生成6种projectile
                }
            }
            if(beamstate){
                beamtimer-=Time.deltaTime;
                if(beamtimer<=0){
                    Destroy(rmobject);
                    beamstate=false;
                    beamtimer=beamtime;
                    Deathray();
                  
                }
            }
        }
        else{
            float distance = Vector2.Distance(targetTransform.position, terminalTransform.position);
            //Debug.Log("Distance: " + distance);
            if(distance<=activedistance){
                activestate=true;
                playercontroller.instance.LenSize = 14;
                Debug.Log("Terminal Active");
            }
        }//判断进入activestate,一旦进入后不再可能退出，直至Terminal死亡

    }
    void beam_action(){
        Vector2 tmp = targetTransform.position - terminalTransform.position;
        float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        tmpx=(float)tmp.x;
        tmpy=(float)tmp.y;
        
        rmobject = Instantiate(beam, terminalTransform.position, targetRotation);
        Debug.Log("Beam Generated");
        beamstate = true;
        MakeSound(0);
    }

    void Deathray()
    {
       
        float angle = Mathf.Atan2(tmpy, tmpx) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject deathray = Instantiate(rm_f, terminalTransform.position, targetRotation);
        MakeSound(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Array")
        {
            HP--;
            Instantiate(hurteffect, transform.position, Quaternion.identity);
        }
    }

    void MakeSound(int n)
    {
        aud.clip = clips[n];
        aud.Play();
    }
}
