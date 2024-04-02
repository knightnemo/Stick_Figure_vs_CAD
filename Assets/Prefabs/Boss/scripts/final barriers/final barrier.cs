using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalbarrier : MonoBehaviour
{
    public int num = 1;
    bool chosen = false;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.2f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Bosslaser")
        {
            
            if (playercontroller.instance.barriernum == num)
            {
                Debug.Log("barrier " + num + " is chosen");
                chosen = true;
                rend.material.color = new Color(0.7f, 0.7f, 1.5f);
            }
            
        }
        if (collision.tag == "Bosselm")
        {
            if (playercontroller.instance.barriernum == num)
            {
                if (chosen)
                {
                    playercontroller.instance.barriernum++;
                    Destroy(gameObject);
                }
               
            }
        }
    }
}
