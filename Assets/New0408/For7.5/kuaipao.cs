using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuaipao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playercontroller.instance.kuaipao = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            
    }
}