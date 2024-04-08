using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurteffect : MonoBehaviour
{
    float staytime = 1;
    float staytimer;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.3f, 1, 1);
        staytimer = staytime;
    }

    // Update is called once per frame
    void Update()
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
