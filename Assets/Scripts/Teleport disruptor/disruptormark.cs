using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disruptormark : MonoBehaviour
{
    Renderer rend;

    float staytime = 1.0f;
    float staytimer;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(2.7f, 0, 2.9f);
        staytimer = staytime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        pos=playercontroller.instance.transform.position;
        transform.position = pos;

        if (playercontroller.instance.canTeleport)
        {
            if (staytimer > 0)
            {
                staytimer -= Time.deltaTime;
            }
            else
            {
                staytimer = staytime;
                Destroy(gameObject);
            }
        }
    }
}
