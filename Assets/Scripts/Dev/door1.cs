using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door1 : MonoBehaviour
{
    public Vector2 sendto;
    public bool BossSwicth = false;
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
            if (BossSwicth)
            {
                Bosscontroller.instance.ON = true;
                playercontroller.instance.atdev = false;
            }
            else
            {
                Debug.Log("Boss off");
                Bosscontroller.instance.ON = false;
                playercontroller.instance.atdev = true;
            }
            
            playercontroller.instance.transform.position = sendto;
            
            Destroy(gameObject);
        }
    }
}
