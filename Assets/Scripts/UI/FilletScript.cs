using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilletScript : MonoBehaviour
{
    public GameObject block;
    public GameObject shadow;
    public Text Chances;
    private int chances;
    private bool can;
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
        chances = playercontroller.instance.chances[3];
        can = playercontroller.instance.canFillet;
        block.SetActive(!can);
        shadow.SetActive(can && (chances == 0));
        if (!can)
        {
            Chances.text = "Locked";
        }
        else
        {
            Chances.text = chances.ToString() + "/" + playercontroller.instance.filletChances.ToString();
        }
    }
}
