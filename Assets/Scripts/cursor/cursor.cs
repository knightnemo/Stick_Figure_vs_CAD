using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    //teleport
    Transform telepointer;
    //Break
    Transform breakpointer;
    // Start is called before the first frame update
    void Start()
    {
        telepointer = GameObject.Find("teleportsign").GetComponent<Transform>();
        breakpointer=GameObject.Find("breaksign").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousepos;

        Showtelepointer();
        Showbreakpointer();
    }

    void Showtelepointer()
    {
        if (playercontroller.instance.rdyfortele == 1)
        {
            telepointer.gameObject.SetActive(true);
        }
        else
        {
            telepointer .gameObject.SetActive(false);
        }
    }
    void Showbreakpointer()
    {
        if(playercontroller.instance.rdyforbreak == 1)
        {
            breakpointer .gameObject.SetActive(true);
        }
        else
        {
            breakpointer.gameObject.SetActive(false);
        }
    }
}
