using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dev : MonoBehaviour
{
    public static dev instance { get; private set; }
    public bool stage = false;
    public GameObject talk1;
    float time1 = 20;
    float timer1;
    bool istalk = false;
    bool cangive = true;
    bool canput = true;
    public GameObject bottle;
    public GameObject door1;
    Vector2 mousepos;

    float time2 = 33;
    float timer2;
    bool cantalk2 = true;
    public GameObject talk2;
    Animator ani;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer1 = time1;
        timer2 = time2;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stage)
        {
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Mathf.Abs(mousepos.x-transform.position.x)<1 && Mathf.Abs(mousepos.x - transform.position.x) < 3.5f)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if (!istalk)
                    {
                        Debug.Log("Starttalk");
                        Instantiate(talk1, transform.position + Vector3.right * 7.0f, Quaternion.identity);
                        istalk = true;
                    }
                   
                }
                
            }
            if (istalk)
            {
                if (timer1 > 0)
                {
                    timer1 -= Time.deltaTime;
                }
                else
                {
                    if (canput)
                    {
                        Instantiate(door1, transform.position - Vector3.right * 8 , Quaternion.identity);
                        canput = false;
                    }
                    
                    timer1 = time1;
                    istalk = false;
                }
                if (timer1 < 8)
                {
                    if (cangive)
                    {
                        Instantiate(bottle,transform.position-Vector3.right*5-Vector3.up*1, Quaternion.identity);
                        cangive = false;
                    }
                }
            }
        }
        if (stage)
        {
            ani.SetTrigger("A");
            playercontroller.instance.atdev = true;
            if (timer2 > 0)
            {
                timer2 -= Time.deltaTime;
            }
            else
            {
                if (cantalk2)
                {
                    Instantiate(talk2, transform.position + Vector3.right * 7.0f, Quaternion.identity);
                    cantalk2 = false;
                }
            }
            if (timer2 < 4)
            {
                playercontroller.instance.atdev = false;
            }
        }
       
    }
}
