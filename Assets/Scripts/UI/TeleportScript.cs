using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TeleportScript : MonoBehaviour
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
        if(playercontroller.instance==null)
        {
            return;
        }
        chances = playercontroller.instance.chances[1];
        block.SetActive(!playercontroller.instance.canTeleport);
        shadow.SetActive(playercontroller.instance.canTeleport && (chances == 0));
        if (!playercontroller.instance.canTeleport)
        {
            Chances.text = "Locked";
        }
        else
        {
            Chances.text = chances.ToString() + "/"+playercontroller.instance.teleportChances.ToString();
        }
    }
}
