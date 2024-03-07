using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)) 
        {
            cam.m_Lens.OrthographicSize++;
        }
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            cam.m_Lens.OrthographicSize--;
        }
    }
}
