using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public bool active = true;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.tag == "Player")
            {
                playercontroller.instance.Destroyed();
            }
        }
        if (collision.tag == "fillet")
        {
            active = false;
            ani.SetTrigger("Filleted");
            
        }
    }
    
}
