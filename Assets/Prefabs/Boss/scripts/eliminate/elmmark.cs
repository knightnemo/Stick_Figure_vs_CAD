using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elmmark : MonoBehaviour
{
    float staytime = 20.0f;
    float staytimer;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;

        transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(3.5f, 1.5f, 1f);
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
