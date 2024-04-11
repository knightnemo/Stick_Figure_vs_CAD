using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powereffect : MonoBehaviour
{
    float size = 1.5f;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.7f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        size -= 0.05f;
        transform.localScale = new Vector3(size, size, 0);
        if (size < 0.1)
        {
            Destroy(gameObject);
        }
    }
}
