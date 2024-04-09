using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneSwitchScript : MonoBehaviour
{
    public static SceneSwitchScript instance { get; private set; }
    public GameObject[] objectsToRemain;//转场时要保留的游戏对象，editor中拖拽
    public string[] scenes;//所有的场景，editor中拖拽
    public int sceneNum;//切换到第sceneNum个场景
    public NextScript next;
    bool resetpos = false;
    float posrestime = 0.2f;
    float posrestimer;

    //End delete
    public bool END=false;
    public float endtime = 2.0f;
    float endtimer;
    public int endtype = 1;

    public AudioClip[] clips;
    public AudioSource aud;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToRemain)//保留其他需要的游戏对象，如Canvas和LogicManager
        {
            DontDestroyOnLoad(obj);
        }
        sceneNum = 0;

        posrestimer = posrestime;
        endtimer = endtime;
        aud = GetComponent<AudioSource>();
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //QuitGame();
        }

        if (playercontroller.instance !=null)
        {
            if (playercontroller.instance.rdytoconc)
            {
                END = true;
            }
        }
        if (END)
        {
            if (endtimer > 0)
            {
                endtimer -= Time.deltaTime;
            }
            else
            {
                endtimer = endtime;
                endtype=playercontroller.instance.type;
                SceneChange();
                
                //Destroy(gameObject);
            }
            if (portrait.instance != null)
            {
                Debug.Log("End is set!");
                portrait.instance.portraittype = endtype;
            }
        }
    }
    public void SceneChange()//场景变换函数
    {
        sceneNum++;
        if (sceneNum > scenes.Length)
        {
            sceneNum=scenes.Length;
        }
        SceneManager.LoadScene(scenes[sceneNum]);
        Debug.Log("sceneNum=" + sceneNum);
        if (sceneNum > 1)
        {
            aud.clip = clips[sceneNum - 2];
            aud.Play();
        }
        LogicScript.instance.startPos=new Vector2 (0,0);
        
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
