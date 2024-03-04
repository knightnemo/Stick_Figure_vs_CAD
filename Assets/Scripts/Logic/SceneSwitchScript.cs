using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public GameObject[] objectsToRemain;//转场时要保留的游戏对象，editor中拖拽
    public string[] scenes;//所有的场景，editor中拖拽
    public int sceneNum;//切换到第sceneNum个场景
    public NextScript next;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToRemain)//保留其他需要的游戏对象，如Canvas和LogicManager
        {
            DontDestroyOnLoad(obj);
        }
        sceneNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(NextScript.instance != null)
        {
            next = NextScript.instance;
        }
        if(Input.GetKeyDown(KeyCode.Backspace))//转场条件，关卡中可以设为OnTriggerEnter2D
        {
            SceneChange();
        }
        if(next.goNext)
        {
            SceneChange();
        }
    }
    void SceneChange()//场景变换函数
    {
        sceneNum++;
        SceneManager.LoadScene(scenes[sceneNum]);
        Debug.Log("sceneNum=" + sceneNum);
    }
}
