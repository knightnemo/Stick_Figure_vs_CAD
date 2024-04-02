using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safezone : MonoBehaviour
{
    float staytime = 20.0f;
    float staytimer;

    public GameObject stand;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;
        Instantiate(stand,transform.position-3*Vector3.up,Quaternion.identity);
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
            Destroy(gameObject);
        }

        if(Mathf.Abs(playercontroller.instance.transform.position.x - transform.position.x)<=4 && Mathf.Abs(playercontroller.instance.transform.position.y - transform.position.y) <= 4)
        {
            Debug.Log("Player is safe now");
            playercontroller.instance.EliminateSafe = true;
        }
        else
        {
            Debug.Log("Player is unsafe now");
            playercontroller.instance.EliminateSafe = false;
        }
    }
}
