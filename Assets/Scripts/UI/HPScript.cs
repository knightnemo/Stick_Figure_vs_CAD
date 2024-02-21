using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    public int hp;
    public GameObject[] hps;
    public Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        hpText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.instance == null)
        {
            return;
        }
        hp = playercontroller.instance.HP;
        hpText.text="HP:"+hp.ToString();
        for(int i=0;i<5;i++)
        {
            hps[i].SetActive(hp > i);
        }
        
    }
}
