using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class area : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend= GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.2f, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance.barriernum == 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.Destroyed();
        }
    }
}
