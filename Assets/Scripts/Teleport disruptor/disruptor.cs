using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disruptor : MonoBehaviour
{
    public float activedistance = 10.0f;

    public GameObject mark;
    bool canmark = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Disrupt();
    }

    void Disrupt()
    {
        Vector2 distance = playercontroller.instance.transform.position - transform.position;
        if (distance.magnitude >= activedistance)
        {
            playercontroller.instance.canTeleport = true;
            Debug.Log("Now can teleport");
            canmark = true;
        }
        else
        {
            playercontroller.instance.canTeleport = false;
            Debug.Log("Disrupt teleport!");
            if (canmark)
            {
                GameObject dismark = Instantiate(mark, transform.position, Quaternion.identity);
                canmark = false;
            }
        }
    }
}
