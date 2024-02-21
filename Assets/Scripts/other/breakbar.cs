using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakbar : MonoBehaviour
{
    public float breaktime = 1.0f;
    float timer;
    bool broken = false;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        timer = breaktime;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (broken)
        {
            timer = timer - Time.deltaTime;

        }
        if (timer <= 0)
        {
            timer = breaktime;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "feet" && collision.tag!="cursor")
        {
            broken = true;
            ani.SetTrigger("Break");
        }
        
    }
}
