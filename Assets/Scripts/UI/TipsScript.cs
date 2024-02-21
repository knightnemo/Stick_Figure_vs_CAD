using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsScript : MonoBehaviour
{
    public LogicScript logic;
    public float[] height;//提示框移动高度，3个高度就够了，为了防止溢出多设置了几个
    public int tipNum=1;
    public Text content;
    public int n;//使用第n个技能
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        logic = LogicScript.instance;
        if(n<4)
        {
            content.text = "You have used:" + logic.skillNames[n] + ". Chances Left:" + playercontroller.instance.chances[n].ToString();//提示词
        }
        if(n==4)
        {
            content.text ="Get hurt. HP"+Console0Script.instance.hurt.ToString();
        }
        if(n==5)
        {
            content.text = "You died.Press Z or the button to resqwan";
        }
        //可以增加新的提示词
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (tipNum <= 3)//最新的3个tips从下往上，多于3个上面的消失
        {
            transform.position = new Vector3(transform.position.x, height[tipNum], 0);
        }
        else
        {
            Destroy(gameObject);
        }   
        StartTimer();
    }
    IEnumerator StartTimer()//定时3.8s消失
    {
        while (timer < 3.8f)
        {
            yield return new WaitForSeconds(0.1f);
            timer+=0.1f;
        }
        Destroy(gameObject);
    }
}
