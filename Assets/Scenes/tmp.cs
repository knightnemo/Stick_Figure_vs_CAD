using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp : MonoBehaviour
{
    public GameObject[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            MakeSound(0);
        }
        if(Input.GetKeyUp(KeyCode.R)) { MakeSound(1); }
        if(Input.GetKeyUp(KeyCode.S)) { MakeSound(2); }
    }
    void MakeSound(int n)
    {
        sounds[n].GetComponent<AudioSource>().Play();
    }
}
