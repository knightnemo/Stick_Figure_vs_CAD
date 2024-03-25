using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getkillall : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1f, 1.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playercontroller.instance.rdy1)
            {
                playercontroller.instance.cankillall = true;
                Destroy(gameObject);
            }
            
        }
    }
}
