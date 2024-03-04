using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public GameObject[] objectsToRemain;//ת��ʱҪ��������Ϸ����editor����ק
    public string[] scenes;//���еĳ�����editor����ק
    public int sceneNum;//�л�����sceneNum������
    public NextScript next;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToRemain)//����������Ҫ����Ϸ������Canvas��LogicManager
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
        if(Input.GetKeyDown(KeyCode.Backspace))//ת���������ؿ��п�����ΪOnTriggerEnter2D
        {
            SceneChange();
        }
        if(next.goNext)
        {
            SceneChange();
        }
    }
    void SceneChange()//�����任����
    {
        sceneNum++;
        SceneManager.LoadScene(scenes[sceneNum]);
        Debug.Log("sceneNum=" + sceneNum);
    }
}
