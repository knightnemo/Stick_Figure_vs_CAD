using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portrait : MonoBehaviour
{
    public static portrait instance { get; private set; }
    Animator ani;
    public int portraittype=0;
    private void Awake()
    {
        instance = this;
        ani=GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (portraittype == 1)
        {
            ani.SetTrigger("normal");
        }
        if (portraittype == 2)
        {
            ani.SetTrigger("Genshin");
        }
        if (portraittype == 3)
        {
            ani.SetTrigger("dev");
        }
    }
}
