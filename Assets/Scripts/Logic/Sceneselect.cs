using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneselect : MonoBehaviour
{
    Animator ani;
    Vector2 mousepos;
    Vector2 defaultpos = new Vector2(0, 0);
    public int num = 1;
    // Start is called before the first frame update
    void Start()
    {
        ani= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(mousepos.x - transform.position.x) < 10f && Mathf.Abs(mousepos.y - transform.position.y) < 2.5f)
        {
            ani.SetBool("Selected", true);
            if (Input.GetMouseButton(0))
            {
                SceneSwitchScript.instance.sceneNum=num;
                LogicScript.instance.startPos = defaultpos;
                SceneManager.LoadScene(SceneSwitchScript.instance.scenes[num]);
                SceneSwitchScript.instance.aud.clip = SceneSwitchScript.instance.clips[num - 2];
                SceneSwitchScript.instance.aud.Play();
            }
        }
        else
        {
            ani.SetBool("Selected", false);
        }
    }
}
