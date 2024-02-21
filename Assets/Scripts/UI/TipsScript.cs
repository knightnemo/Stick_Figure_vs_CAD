using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsScript : MonoBehaviour
{
    public LogicScript logic;
    public float[] height;//��ʾ���ƶ��߶ȣ�3���߶Ⱦ͹��ˣ�Ϊ�˷�ֹ����������˼���
    public int tipNum=1;
    public Text content;
    public int n;//ʹ�õ�n������
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        logic = LogicScript.instance;
        if(n<4)
        {
            content.text = "You have used:" + logic.skillNames[n] + ". Chances Left:" + playercontroller.instance.chances[n].ToString();//��ʾ��
        }
        if(n==4)
        {
            content.text ="Get hurt. HP"+Console0Script.instance.hurt.ToString();
        }
        if(n==5)
        {
            content.text = "You died.Press Z or the button to resqwan";
        }
        //���������µ���ʾ��
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (tipNum <= 3)//���µ�3��tips�������ϣ�����3���������ʧ
        {
            transform.position = new Vector3(transform.position.x, height[tipNum], 0);
        }
        else
        {
            Destroy(gameObject);
        }   
        StartTimer();
    }
    IEnumerator StartTimer()//��ʱ3.8s��ʧ
    {
        while (timer < 3.8f)
        {
            yield return new WaitForSeconds(0.1f);
            timer+=0.1f;
        }
        Destroy(gameObject);
    }
}
