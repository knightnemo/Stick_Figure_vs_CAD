using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text self;
    // Start is called before the first frame update
    void Start()
    {
        self= GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LogicScript.instance != null)
        {
            self.text = LogicScript.instance.finalScore.ToString();
        }
    }
}
