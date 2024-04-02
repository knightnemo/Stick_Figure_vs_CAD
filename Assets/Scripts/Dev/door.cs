using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    float staytime = 3.0f;
    float staytimer;
 
    bool canput = true;
    public GameObject talk2;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;
    }

    // Update is called once per frame
    void Update()
    {
        if(staytimer > 0)
        {
            staytimer -= Time.deltaTime;
        }
        else
        {
            staytimer = staytime;
            Destroy(gameObject);
        }

        if (staytimer < 2)
        {
            if (canput)
            {
                Debug.Log("dev come out!");
                dev.instance.transform.position = transform.position + Vector3.right * 1.5f;
                Instantiate(talk2,transform.position+Vector3.right*7.5f, Quaternion.identity);
                canput = false;
            }
        }
    }
}
