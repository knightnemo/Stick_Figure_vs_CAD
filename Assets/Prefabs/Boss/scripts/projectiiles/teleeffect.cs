using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleeffect : MonoBehaviour
{
    float size = 0.2f;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.7f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        size += 0.2f;
        transform.localScale = new Vector3(size, size, 0);
        if (size > 10)
        {
            Destroy(gameObject);
        }
    }
}
