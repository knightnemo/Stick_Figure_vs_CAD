using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakzone : MonoBehaviour
{
    public float magtime = 0.01f;
    float magtimer;
    public float maxscale = 6.0f;
    float scale = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (magtimer > 0)
        {
            magtimer -= Time.deltaTime;
        }
        else
        {
            scale = scale + 0.03f;
            magtimer = magtime;
        }
        transform.localScale = new Vector3(scale, scale, 1);
        if (scale >= maxscale)
        {
            Destroy(gameObject);
        }
    }
}
