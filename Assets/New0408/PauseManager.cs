using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance {  get; private set; } 
    public bool isPause;
    public GameObject canvas;
    public GameObject exiting;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)&&!isPause)
        {
            isPause=true;
            PauseGame(isPause);
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&isPause)
        {
            exiting.SetActive(true);
            StartCoroutine(Count());
            Debug.Log("Quit");
            Debug.Log(Time.time);
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Space)&&isPause)
        {
            isPause = false;
            PauseGame(isPause);
        }
        if(Input.GetKeyDown(KeyCode.Return)&&isPause)
        {
            isPause = false;
            PauseGame(isPause);
            canvas.SetActive(false);
            SceneSwitchScript.instance.sceneNum = 1;
            SceneManager.LoadScene("Select");
            SceneSwitchScript.instance.aud.Stop();
            
        }
    }

    public void PauseGame(bool isPause)
    {
        canvas.SetActive(isPause);
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void UnPause()
    {
        isPause = false;
        PauseGame(isPause);
    }



    IEnumerator Count()
    {
        float startTime = Time.realtimeSinceStartup;
        Debug.Log(startTime); // 打印当前时间

        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            yield return null; // 等待下一帧
        }

        Debug.Log("5 seconds have passed."); // 5秒钟后打印消息
    }
}
