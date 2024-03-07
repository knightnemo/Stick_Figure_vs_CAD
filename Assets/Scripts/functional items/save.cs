using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour
{
    Renderer rend;
    Color texturecolor;
    float shinetime = 0.5f;
    float shinetimer;
    int litup = 1;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        
        shinetimer = shinetime;
    }

    // Update is called once per frame
    void Update()
    {
        Shine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playercontroller.instance.TakedownSavepoint();
            Destroy(gameObject);
        }
    }

    void Shine()
    {
        if (shinetimer > 0)
        {
            shinetimer -= Time.deltaTime;
        }
        else
        {
            shinetimer = shinetime;
            litup = litup * -1;

        }
        if (litup == 1)
        {
            texturecolor = new Color(1f, 1.3f, 1f);

        }
        if (litup == -1)
        {
            texturecolor = new Color(1f, 1f, 1f);

        }

        rend.material.color = texturecolor;
    }
}
