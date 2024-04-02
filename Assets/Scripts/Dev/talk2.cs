using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talk2 : MonoBehaviour
{
    float staytime = 19;
    float staytimer;
    public GameObject cmd;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;
    }

    // Update is called once per frame
    void Update()
    {
        if (staytimer > 0)
        {
            staytimer -= Time.deltaTime;
        }
        else
        {
            staytimer = staytime;
            Instantiate(cmd,transform.position+Vector3.right*8,Quaternion.identity);
            Destroy(gameObject);
            
        }
    }
}
