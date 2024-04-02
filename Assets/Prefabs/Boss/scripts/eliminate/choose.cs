using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choose : MonoBehaviour
{
    float staytime = 20.0f;
    float staytimer;
    float waittime = 10.0f;
    float waittimer;
    bool canplace=true;
    bool canrelease=true;
    public GameObject greenzone;
    public GameObject killzone;
    // Start is called before the first frame update
    void Start()
    {
        staytimer = staytime;
        waittimer=waittime;
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

        if (canplace)
        {
            float x, y;
            int direction;
            
            for(int i=1;i<=1;i++)
            {
                direction = Random.Range(1, 3);
                if (direction % 2 == 0)
                {
                    x = Random.Range(-10, -5);
                }
                else
                {
                    x = Random.Range(5, 10);
                }
                direction = Random.Range(1, 3);
                if (direction % 2 == 0)
                {
                    y = Random.Range(-10, -5);
                }
                else
                {
                    y = Random.Range(5, 10);
                }
                Instantiate(greenzone, transform.position + y * Vector3.up*3 + x * Vector3.right*3, Quaternion.identity);
            }
            canplace = false;
        }

        if (waittimer > 0)
        {
            waittimer-= Time.deltaTime;
        }
        else
        {
            waittimer = waittime;
            if (canrelease)
            {
                Instantiate(killzone, transform.position, Quaternion.identity);
                canrelease = false;
            }
            
        }
    }
}
