using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectzone : MonoBehaviour
{
    public float magtime = 0.02f;
    float magtimer;
    public float maxscale = 5.0f;
    float scale = 0.2f;

    bool canbreak = false;
    public GameObject diss;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousepos;
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
            scale = scale + 0.2f;
            magtimer = magtime;
        }
        transform.localScale = new Vector3(scale, scale, 1);
        if (scale >= maxscale)
        {
            if (canbreak)
            {
                Break();
                playercontroller.instance.chances[2]--;
                Debug.Log("Release the blue zone!");
                canbreak = false;
            }
            else
            {
                Debug.Log("No Array in range!");
                //playercontroller.instance.rdyforbreak = 1;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Array")
        {
            canbreak = true;
        }
    }

    void Break()
    {
        GameObject dissobject = Instantiate(diss, transform.position, Quaternion.identity);
    }
}
