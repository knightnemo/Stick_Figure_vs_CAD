using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectmark : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1f, 1.1f, 2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=playercontroller.instance.transform.position;
        if (!playercontroller.instance.Selected)
        {
            Destroy(gameObject);
        }
    }
}
