using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathbeam : MonoBehaviour
{
    public float magtime = 0.1f;
    float magtimer;
    float scale = 0.1f;
    bool magfinish = false;

    Renderer rend;
    Color texturecolor;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<Renderer>();
        magtimer = magtime;
        texturecolor = new Color(1.5f, 1, 1);
        rend.material.color= texturecolor;
    }

    // Update is called once per frame
    void Update()
    {
        if (magtimer > 0)
        {
            magtimer-= Time.deltaTime;
        }
        else
        {
            if (!magfinish)
            {
                scale = scale + 0.1f;
            }
            else
            {
                scale = scale -= 0.1f;
            }
            magtimer = magtime;
        }
        transform.localScale = new Vector3(10, scale, 1);
        if (scale >= 1)
        {
            magfinish = true;
        }
        if (scale <= 0)
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
