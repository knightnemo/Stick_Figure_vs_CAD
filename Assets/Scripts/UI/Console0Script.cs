using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console0Script : MonoBehaviour
{
    public GameObject template;//���Ƶ�ģ��
    public GameObject[] tips;
    public TipsScript tempScript;//��ʱ���ʸ�������Ľű�
    public int hurt;//�ܵ��˺�
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
        tips = GameObject.FindGameObjectsWithTag("tips");//ʵʱ��ȡ�ڳ�tips
    }
    public void Generate(int n)//���ɵ�n����ʾ
    {
        GameObject neo = Instantiate(template, new Vector3(715f, 130f, 0f), transform.rotation, transform);
        TipsScript temp = neo.GetComponent<TipsScript>();
        temp.n = n;
        TipPlus();
    }
    public void TipPlus()//����tips����������Ҫminus����ΪtipNumֻ��������������壬����3�Ķ�����ʧ�ͺ�
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
