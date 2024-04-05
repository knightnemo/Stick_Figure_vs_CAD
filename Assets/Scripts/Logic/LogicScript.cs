using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public static LogicScript instance {  get; private set; }
    public string[] skillNames;
    public Vector2 startPos;
    public int deathNum=0;
    public int lastScore = 0;
    public int finalScore = 0;
    public Text Score_;
    public Text Deaths_;
    
    private void Awake()
    {
        instance = this; 
    }
    private void Update()
    {
        if (Score_ != null)
        {
            Score_.text = "Score:" + finalScore;
        }
        if(Deaths_ != null)
        {
            Deaths_.text = "Deaths:" + deathNum;
        }
    }
    public void NewScore()
    {
        lastScore = finalScore;
        finalScore = 0;
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
