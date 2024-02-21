using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EraseScript : MonoBehaviour
{
    public GameObject block;
    public GameObject shadow;
    public Text Chances;
    private int chances;
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
        chances = playercontroller.instance.chances[0];
        block.SetActive(!playercontroller.instance.canErase);
        shadow.SetActive(playercontroller.instance.canErase&&(chances==0));
        if(!playercontroller.instance.canErase)
        {
            Chances.text = "Locked";
        }
        else
        {
            Chances.text=chances.ToString()+ "/" + playercontroller.instance.eraseChances.ToString();
        }
    }
}