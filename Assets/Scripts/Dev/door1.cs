using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door1 : MonoBehaviour
{
    public Vector2 sendto;
    public bool BossSwicth = true;
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
            playercontroller.instance.transform.position = sendto;
            Bosscontroller.instance.ON = BossSwicth;
            Destroy(gameObject);
        }
    }
}
