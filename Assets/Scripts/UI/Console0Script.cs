using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console0Script : MonoBehaviour
{
    public GameObject template;//复制的模板
    public GameObject[] tips;
    public TipsScript tempScript;//临时访问各个对象的脚本
    public int hurt;//受到伤害
    public static Console0Script instance { get; private set; }
    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tips = GameObject.FindGameObjectsWithTag("tips");//实时获取在场tips
    }
    public void Generate(int n)//生成第n条提示
    {
        GameObject neo = Instantiate(template, new Vector3(715f, 130f, 0f), transform.rotation, transform);
        TipsScript temp = neo.GetComponent<TipsScript>();
        temp.n = n;
        TipPlus();
    }
    public void TipPlus()//增加tips数量，不需要minus，因为tipNum只对自身对象有意义，大于3的对象消失就好
    {
        if(tips!=null)
        {
            foreach (GameObject tip in tips)
            {
                tempScript = tip.GetComponent<TipsScript>(); 
                tempScript.tipNum++;
            }
            tips = GameObject.FindGameObjectsWithTag("tips");
        }
        
    }
}
