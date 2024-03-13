using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetdetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When step on ground
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ground" || collider.tag=="Array"|| collider.tag== "chosenbar" || collider.tag == "object")
        {
            //Debug.Log("Step on gound");
            playercontroller.instance.canjump = true;
            playercontroller.instance.canjumptwice = false;
            playercontroller.instance.intheair = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "ground" || collider.tag == "Array" || collider.tag == "chosenbar" ||collider.tag=="object")
        {
            //Debug.Log("Step on gound");
            playercontroller.instance.canjump = true;
            playercontroller.instance.canjumptwice = false;
            playercontroller.instance.intheair = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "ground" || collider.tag == "Array" || collider.tag == "chosenbar" || collider.tag == "object")
        {
            //Debug.Log("Away from gound");
            playercontroller.instance.canjump = false;
            playercontroller.instance.intheair = true;
            playercontroller.instance.canjumptwice = true;
        }
    }
}
