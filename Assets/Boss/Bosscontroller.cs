using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosscontroller : MonoBehaviour
{
    public static Bosscontroller instance {  get; private set; }
    public playercontroller player;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        player = playercontroller.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move()
    {
        
    }
}
