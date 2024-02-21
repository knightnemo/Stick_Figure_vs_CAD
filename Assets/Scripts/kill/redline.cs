using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.Destroyed();
        }
        if (collision.tag == "paperchar")
        {
            paperchar ch = collision.GetComponent<paperchar>();
            ch.selfdestroy();
        }
    }

}
