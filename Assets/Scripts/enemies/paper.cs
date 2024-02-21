using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paper : MonoBehaviour
{
    public GameObject paperchar;
    public float papercharttime = 1.0f;
    float paperchartimer;
    public float firedistance = 20.0f;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        paperchartimer = papercharttime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
        if (paperchartimer > 0)
        {
            paperchartimer = paperchartimer - Time.deltaTime;
        }
        else
        {
            if (distance.magnitude <= firedistance)
            {
                Fire();
                paperchartimer = papercharttime;
            }
        }
    }

    void Fire()
    {
        Vector2 direction = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
        direction.Normalize();
        GameObject papercharobject = Instantiate(paperchar, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        paperchar papercharproj = papercharobject.GetComponent<paperchar>();
        papercharproj.launch(direction);
    }
}
