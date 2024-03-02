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
    float force=10.0f;
    bool activestate=false;
    public GameObject ply;
    public float activedistance;
    public float interval=5.0f;
    public float projectilespeed;
    Vector2 direction;
    float intervaltime;
    Transform targetTransform;
    Rigidbody2D rigidbody2d;
    public GameObject rmobject;
    bool beamstate;
    public float beamtime;
    Vector2 copydir;
    float beamtimer;
    // Start is called before the first frame update
    void Start()
    {
        terminalTransform = GetComponent<Transform>();
        targetTransform = ply.transform;
        intervaltime=interval;
        beamstate=false;
        beamtimer=beamtime;
    }

    // Update is called once per frame
    void Update()
    {//FIXME:预留接口，如果有一二阶段切换可以通过调整randomNumber的范围和intervaltime的数值实现
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
                    }
                    else if(randomNumber<=8){
                        rmobject = Instantiate(tac, (Vector2)terminalTransform.position, Quaternion.identity);
                        rigidbody2d =tac.GetComponent<Rigidbody2D>();
                        direction.x=targetTransform.position.x-terminalTransform.position.x;
                        direction.y=targetTransform.position.y-terminalTransform.position.y;
                        rigidbody2d.AddForce(-direction * force);//tac
                    }
                    else if(randomNumber<=10){
                    rmobject = Instantiate(wq, (Vector2)terminalTransform.position, Quaternion.identity);
                    rigidbody2d =wq.GetComponent<Rigidbody2D>();
                    direction.x=targetTransform.position.x-terminalTransform.position.x;
                    direction.y=targetTransform.position.y-terminalTransform.position.y;
                    rigidbody2d.AddForce(-direction * force);//wq
                    }
                    else{
                        rmobject = Instantiate(move, (Vector2)terminalTransform.position, Quaternion.identity);
                        rigidbody2d =move.GetComponent<Rigidbody2D>();
                        direction.x=targetTransform.position.x-terminalTransform.position.x;
                        direction.y=targetTransform.position.y-terminalTransform.position.y;
                        rigidbody2d.AddForce(-direction * force);//move
                    }//随机生成6种projectile
                }//FIXME:没有对应的小怪，help暂时搁弃
            }
            if(beamstate){
                beamtimer-=Time.deltaTime;
                if(beamtimer<=0){
                    Destroy(rmobject);
                    beamstate=false;
                    beamtimer=beamtime;
                    rmobject = Instantiate(rm_f, (Vector2)terminalTransform.position, Quaternion.identity);
                    rigidbody2d =rm_f.GetComponent<Rigidbody2D>();
                    rigidbody2d.AddForce(-copydir * force);
                }
            }
        }
        else{
            float distance = Vector2.Distance(targetTransform.position, terminalTransform.position);
            //Debug.Log("Distance: " + distance);
            if(distance<=activedistance){
                activestate=true;
                Debug.Log("Terminal Active");
            }
        }//判断进入activestate,一旦进入后不再可能退出，直至Terminal死亡

    }
    void beam_action(){
        Vector2 tmp = targetTransform.position - terminalTransform.position;
        float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float tmpx=(float)tmp.x;
        float tmpy=(float)tmp.y;
        copydir=new Vector2(tmpx, tmpy);
        rmobject = Instantiate(beam, terminalTransform.position, targetRotation);
        Debug.Log("Beam Generated");
        beamstate = true;
    }
}
