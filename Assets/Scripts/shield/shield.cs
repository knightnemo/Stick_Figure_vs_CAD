using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    public float staydistance = 1.0f;

    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerpos = playercontroller.instance.transform.position;
        Vector2 direction=(mousepos - playerpos).normalized;
        transform.right = direction;
        transform.position = playerpos + direction * staydistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="C++error" || collision.tag == "C#error" ||collision.tag=="Cerror" ||collision.tag=="terminalchar")
        {
            ani.SetTrigger("Shieldhit");
        }
    }
}
