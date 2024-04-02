using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    float CurrentSize=8.0f;
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
            playercontroller.instance.LenSize++;
        }
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            playercontroller.instance.LenSize--;
        }

        
        if(CurrentSize- playercontroller.instance.LenSize>0.1f)
        {
            CurrentSize -= 0.05f;
        }
        if (CurrentSize - playercontroller.instance.LenSize < -0.1f)
        {
            CurrentSize += 0.05f;
        }
        cam.m_Lens.OrthographicSize = CurrentSize;
    }
}
