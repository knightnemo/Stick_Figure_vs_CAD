using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldScript : MonoBehaviour
{
    public GameObject block;
    public GameObject shadow;
    public Text Chances;
    //private int chances;
    // Start is called before the first frame update
    void Start()
    {
        Chances = transform.Find("Chances").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance == null)
        {
            return;
        }
        //chances = playercontroller.instance.chances[0];
        //block.SetActive(!playercontroller.instance.canErase);
        shadow.SetActive(playercontroller.instance.shieldnum==0 );
        if (playercontroller.instance.shieldnum==0)
        {
            Chances.text = "None";
        }
        else
        {
            Chances.text = playercontroller.instance.shieldnum.ToString();
        }
    }
}
