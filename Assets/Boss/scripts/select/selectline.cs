using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectline : MonoBehaviour
{

    float staytime = 1.0f;
    float staytimer;

    float scale = 0.1f;

    public GameObject selectbox;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;

        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1f, 1.1f, 2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 p = Bosscontroller.instance.transform.position;
        p.y += 16;
        transform.position = p;

        if (staytimer > 0)
        {
            staytimer -= Time.deltaTime;
            transform.Rotate(Vector3.forward, 0.001f);
        }
        else
        {
            staytimer = staytime;
            Destroy(gameObject);
        }

        if (staytimer > 0.8f)
        {
            if (scale < 1)
            {
                scale += 0.05f;
                transform.localScale = new Vector3(scale * 40, scale, 0);
            }

        }

        if (staytimer < 0.2f)
        {
            if (scale > 0)
            {
                scale -= 0.05f;
                transform.localScale = new Vector3(scale * 40, scale, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            playercontroller.instance.Selected = true;
            Instantiate(selectbox, playercontroller.instance.transform.position,Quaternion.identity);

        }
        if (collision.tag == "barrier")
        {
            Debug.Log("Choose a barrier");
        }
    }
}
