using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartHappening : MonoBehaviour
{
    public static RestartHappening instance {  get; private set; }
    public GameObject restart;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        restart=Instantiate(restart, transform);
        restart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
