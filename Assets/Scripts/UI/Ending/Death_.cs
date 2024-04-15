using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death_ : MonoBehaviour
{
    Text self;
    int count;
    float timer = 0f;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.instance != null)
        {
            count = LogicScript.instance.deathNum;
        }
        if (timer < 0.1f)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 0.1f)
        {
            timer = 0f;
            self.text = "Deaths:" + i.ToString();
        }
    }

}
