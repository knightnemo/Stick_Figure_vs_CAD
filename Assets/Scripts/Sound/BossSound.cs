using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    public Bosscontroller boss;
    public GameObject[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        boss=Bosscontroller.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            if (boss.isMulti)
            {
                MakeSound(0);
            }
            if (boss.isDirect)
            {
                MakeSound(1);
            }
            if (boss.isLaser)
            {
                MakeSound(2);
            }
            if (boss.isF)
            {
                MakeSound(3);
            }
            if (boss.iselm)
            {
                MakeSound(4);
            }
            if (boss.isselect)
            {
                MakeSound(5);
            }
            if (boss.isBlow)
            {
                MakeSound(6);
            }
        }
    }
    public void MakeSound(int n)
    {
        sounds[n].GetComponent<AudioSource>().Play();
        Debug.Log("PlayMusic: " + sounds[n].name);
    }

}
