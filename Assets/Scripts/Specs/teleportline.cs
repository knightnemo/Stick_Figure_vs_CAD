using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportline : MonoBehaviour
{
    float scale=1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 mousepos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerpos=playercontroller.instance.transform.position;
        transform.position = new Vector2((mousepos.x + playerpos.x)/2,(mousepos.y + playerpos.y)/2);
        transform.right=new Vector2(mousepos.x-playerpos.x,mousepos.y-playerpos.y).normalized;
        scale = new Vector2(mousepos.x - playerpos.x, mousepos.y - playerpos.y).magnitude/6;
        transform.localScale = new Vector3(scale,1,1);

    }
}
