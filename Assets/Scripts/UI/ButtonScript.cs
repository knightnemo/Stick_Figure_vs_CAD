using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button button;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Restart();
        }
        
    }
    void OnButtonClick()
    {
        Restart();
        //playercontroller.instance.died = false;
    }
    public void Restart()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        playercontroller.instance.died = false;
        if (Console0Script.instance?.tips != null)
        {
            foreach (GameObject tip in Console0Script.instance?.tips)
            {
                Destroy(tip);
            }
        }
        //Debug.Log("Œ“‘⁄");
        Destroy(playercontroller.instance.restart);
        playercontroller.instance.Resqwan();
    }
    
}
