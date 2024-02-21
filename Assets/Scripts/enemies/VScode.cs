using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VScode : MonoBehaviour
{
    public float rotatespeed = 0.2f;
    public float activedistance = 10.0f;
    bool rdysqawn = false;
    public float sqawntime = 1.5f;
    float sqawntimer;
    
    bool cansqawn = false;
    public GameObject sqawn;
    // Start is called before the first frame update
    void Start()
    {
        sqawntimer = sqawntime;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
        
        if (transform.rotation.eulerAngles.z<=91.0f && transform.rotation.eulerAngles.z >= 90.0f)
        {
            
            rdysqawn = true;
            
        }
        if (sqawntimer > 0)
        {
            if (rdysqawn)
            {
                sqawntimer -= Time.deltaTime;
            }
        }
        else
        {
            rdysqawn = false;
            cansqawn = true;
            transform.Rotate(Vector3.forward, rotatespeed*20.0f);
            sqawntimer = sqawntime;
        }
        if (!rdysqawn)
        {
            transform.Rotate(Vector3.forward, rotatespeed);
        }
        if (distance.magnitude <= activedistance)
        {
            if (cansqawn)
            {
                Sqawn();
                cansqawn = false;
            }
        }
        
        
    }

    void Sqawn()
    {
        
        GameObject sqawnobject = Instantiate(sqawn, transform.position + Vector3.up * -0.5f, Quaternion.identity);
    }
}
