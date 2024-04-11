using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    bool activated = false;

    float risetime = 5.0f;
    float risetimer;
    bool arrive = false;
    float waittime = 3.0f;
    float waittimer;
    bool finish = false;
    float lituptime = 5.0f;
    float lituptimer;
    bool canlit = true;
    public GameObject flare;
    public GameObject hole;
    public GameObject shadow;
    SpriteRenderer rend;
    AudioSource aud;
    public AudioClip[] clips;
    bool sound1 = true;
    bool sound2 = true;
    bool sound3 = true;
    // Start is called before the first frame update
    void Start()
    {
        risetimer = risetime;
        waittimer = waittime;
        lituptimer = lituptime;

        rend= GetComponent<SpriteRenderer>();
        aud=  GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance.End)
        {
            activated = true;
        }
        if (activated)
        {
            if (sound1)
            {
                MakeSound(0);
                sound1 = false;
            }
            playercontroller.instance.transform.position = transform.position;
            playercontroller.instance.LenSize = 10;
            SceneSwitchScript.instance.aud.Stop();
        }
        if (activated)
        {
            if (risetimer > 0)
            {
                Vector2 pos = transform.position;
                pos.y += 0.2f;
                transform.position = pos;

                risetimer -= Time.deltaTime;
            }
            else
            {
                arrive = true;
            }
        }
        if (arrive)
        {
            if (waittimer > 0)
            {
                waittimer -= Time.deltaTime;
            }
            else
            {
                finish = true;
            }
            if (sound2)
            {
                MakeSound(1);
                sound2 = false;
            }
        }
        if (finish)
        {
            if (lituptimer > 0)
            {
                lituptimer -= Time.deltaTime;
                if (transform.rotation.eulerAngles.z < 91.0f)
                {
                    transform.Rotate(Vector3.right, 0.20f);
                }
                    
            }
            if(lituptimer>0 && lituptimer < 0.1f)
            {
                if (canlit)
                {
                    Debug.Log("lightup!");
                    Lightup();
                    canlit = false;
                    if (sound3)
                    {
                        MakeSound(2);
                        sound3 = false;
                    }
                }
                
            }
            if (lituptimer <= 0)
            {
                playercontroller.instance.rdytoconc = true;
                Destroy(gameObject);
                Instantiate(hole, transform.position, Quaternion.identity);
                Instantiate(shadow, transform.position, Quaternion.identity);
            }
        }
    }

    void Lightup()
    {
        rend.material.color = new Color(2.0f, 2.0f, 2.0f);
        Instantiate(flare, transform.position, Quaternion.identity);
    }
    public void MakeSound(int n)
    {
        //sounds[n].GetComponent<AudioSource>().Play();
        aud.clip = clips[n];
        aud.Play();
    }
}
