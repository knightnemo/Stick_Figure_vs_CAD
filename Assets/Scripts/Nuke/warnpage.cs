using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warnpage : MonoBehaviour
{
    Animator ani;
    float distance = 19.0f;

    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 d = playercontroller.instance.transform.position - transform.position;
        if (d.magnitude > distance)
        {
            Destroy(gameObject);
        }

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 c = new Vector2(transform.position.x + 1.19f,transform.position.y-2.89f);
        Debug.Log("Pos c is"+c);
        
        if(Mathf.Abs(pos.x-c.x)<3.6f && Mathf.Abs(pos.y - c.y) < 0.82f)
        {
            Debug.Log("selected");
            ani.SetBool("selected", true);
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Genshin,ready for start!");
                playercontroller.instance.rdyforstart = true;
                GameObject firepage=Instantiate(fire,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            ani.SetBool("selected", false);
        }

    }
    
}
