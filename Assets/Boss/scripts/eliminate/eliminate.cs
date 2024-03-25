using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class eliminate : MonoBehaviour
{
    float scale = 0.2f;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.8f,1.3f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        scale += 0.01f;
        transform.localScale=new Vector3 (scale,scale,0);
        transform.Rotate(Vector3.forward, 0.5f);
        if (scale > 15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!playercontroller.instance.EliminateSafe)
            {
                playercontroller.instance.ChangeHP(-1);
            }
        }
        if(collision.tag == "EOL")
        {
            EOLcontroller.instance.HP -= 100;
        }
        if (collision.tag == "barrier")
        {
            Debug.Log("Hit a barrier");
        }
    }
}
