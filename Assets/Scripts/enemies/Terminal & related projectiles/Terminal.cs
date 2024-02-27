using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{   
    Transform terminalTransform;
    public GameObject rm;
    public GameObject wq;
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
    // Start is called before the first frame update
    void Start()
    {
        terminalTransform = GetComponent<Transform>();
        targetTransform = ply.transform;
        intervaltime=interval;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activestate){
            intervaltime-=Time.deltaTime;
            if(intervaltime<=0){
                intervaltime=interval;
                int randomNumber = Random.Range(1, 10);
                if(randomNumber<=6){
                rmobject = Instantiate(rm, (Vector2)terminalTransform.position+ Vector2.up * 3.0f, Quaternion.identity);
                rigidbody2d =rm.GetComponent<Rigidbody2D>();}
                else{
                rmobject = Instantiate(wq, (Vector2)terminalTransform.position+ Vector2.up * 3.0f, Quaternion.identity);
                rigidbody2d =wq.GetComponent<Rigidbody2D>();
                }
                direction.x=targetTransform.position.x-terminalTransform.position.x;
                direction.y=targetTransform.position.y-terminalTransform.position.y;
                rigidbody2d.AddForce(-direction * force);//随机生成:wq或rm
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
}
