using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public GameObject[] objectsToRemain;//转场时要保留的游戏对象，editor中拖拽
    public string[] scenes;//所有的场景，editor中拖拽
    public int sceneNum;//切换到第sceneNum个场景
    public NextScript next;
    bool resetpos = false;
    float posrestime = 0.2f;
    float posrestimer;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToRemain)//保留其他需要的游戏对象，如Canvas和LogicManager
        {
            DontDestroyOnLoad(obj);
        }
        sceneNum = 0;

        posrestimer = posrestime;
    }

    // Update is called once per frame
    void Update()
    {
        if(NextScript.instance != null)
        {
            next = NextScript.instance;
            if (next.goNext)
            {
                SceneChange();
                resetpos = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace))//转场条件，关卡中可以设为OnTriggerEnter2D
        {
            SceneChange();
        }

        if (resetpos)
        {
            if (posrestimer > 0)
            {
                posrestimer -= Time.deltaTime;
            }
            else
            {
                playercontroller.instance.transform.position = new Vector2(0, 0);
                resetpos = false;
                posrestimer = posrestime;
            }
        }

    }
    void SceneChange()//场景变换函数
    {
        sceneNum++;
        SceneManager.LoadScene(scenes[sceneNum]);
        Debug.Log("sceneNum=" + sceneNum);
    }
}
