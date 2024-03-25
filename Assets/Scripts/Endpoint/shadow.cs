using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<SpriteRenderer>();
        rend.material.color = new Color(2f, 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
