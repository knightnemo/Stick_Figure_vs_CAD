using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebar : MonoBehaviour
{
    public bool vertical;
    public float leftpos=1.0f;
    public float rightpos=1.0f;
    public float speed = 2.0f;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (vertical)
        {
            pos.y = pos.y + speed * Time.deltaTime*direction;
            if (pos.y >= rightpos)
            {
                direction = -1;
            }
            if (pos.y <= leftpos)
            {
                direction = 1;
            }
        }
        else
        {
            pos.x = pos.x + speed * Time.deltaTime*direction;
            if (pos.x >= rightpos)
            {
                direction = -1;
            }
            if (pos.x <= leftpos)
            {
                direction = 1;
            }
        }
        transform.position = pos;
    }
}
