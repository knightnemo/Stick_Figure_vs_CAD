using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public static LogicScript instance {  get; private set; }
    public string[] skillNames;
    public Vector2 startPos;
    private void Awake()
    {
        instance = this; 
    }
    /*public void Restart()
    {
        Debug.Log("Restart0");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        playercontroller.instance.died = false;
        if (playercontroller.instance.console?.tips != null)
        {
            foreach (GameObject tip in playercontroller.instance.console?.tips)
            {
                Destroy(tip);
            }
        }
        Destroy(playercontroller.instance.restart);
        
    }
    */
    
}