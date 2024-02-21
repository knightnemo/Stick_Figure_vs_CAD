using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerror : MonoBehaviour
{
    public float magtime = 0.5f;
    float magtimer;
    float scale = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        magtimer = magtime;
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
        if (scale >= 1.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-2);
            Debug.Log("hit the player");
            Vector2 distance = new Vector2(transform.position.x -
            playercontroller.instance.transform.position.x, transform.position.y -
            playercontroller.instance.transform.position.y);
            distance.Normalize();
            playercontroller.instance.Konckback(-distance, 700);
        }
    }
}
