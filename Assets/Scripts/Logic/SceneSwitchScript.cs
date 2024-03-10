using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public GameObject[] objectsToRemain;//ת��ʱҪ��������Ϸ����editor����ק
    public string[] scenes;//���еĳ�����editor����ק
    public int sceneNum;//�л�����sceneNum������
    public NextScript next;
    bool resetpos = false;
    float posrestime = 0.2f;
    float posrestimer;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToRemain)//����������Ҫ����Ϸ������Canvas��LogicManager
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
        if(Input.GetKeyDown(KeyCode.Backspace))//ת���������ؿ��п�����ΪOnTriggerEnter2D
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
    void SceneChange()//�����任����
    {
        sceneNum++;
        SceneManager.LoadScene(scenes[sceneNum]);
        Debug.Log("sceneNum=" + sceneNum);
    }
}
