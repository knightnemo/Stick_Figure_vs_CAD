using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakdownScript : MonoBehaviour
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
        chances = playercontroller.instance.chances[2];
        block.SetActive(!playercontroller.instance.canBreakdown);
        shadow.SetActive(playercontroller.instance.canBreakdown && (chances == 0));
        if (!playercontroller.instance.canBreakdown)
        {
            Chances.text = "Locked";
        }
        else
        {
            Chances.text = chances.ToString() + "/" + playercontroller.instance.breakdownChances.ToString();
        }
    }
}
