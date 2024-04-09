using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnPause : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick()
    {
        PauseManager.instance.isPause=!PauseManager.instance.isPause;
        PauseManager.instance.UnPause();
    }
}
