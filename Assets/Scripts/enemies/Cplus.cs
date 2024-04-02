using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cplus : MonoBehaviour
{
    public float activedistance = 15.0f;
    bool active = false;

    public float firetime = 4.0f;
    float firetimer;
    public GameObject Cpluserror;

    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        firetimer = firetime;
        aud = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
        if (distance.magnitude <= activedistance)
        {
            active = true;
        }
        else
        {
            active = false;
        }

        if (active)
        {
            Aimplayer();
        }

        if (firetimer > 0)
        {
            firetimer -= Time.deltaTime;
        }
        else
        {
            if (active)
            {
                Shoot();
                firetimer = firetime;
            }
               
        }

    }

    void Aimplayer()
    {
        Vector3 v = (playercontroller.instance.transform.position - transform.position).normalized;
        transform.right = v;
    }

    void Shoot()
    {
        aud.Play();
        GameObject Cpluserrorobject = Instantiate(Cpluserror, transform.position, Quaternion.identity);
       
    }
}
