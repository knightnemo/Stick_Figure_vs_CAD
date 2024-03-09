using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPlayer : MonoBehaviour
{
    public float open;
    public float close;
    public GameObject buttP;
    public bool run;
    public float speed;
    bool clone;
    public GameObject canvas;
    Rigidbody2D rb;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        clone = false;   
        run = false;
        rb= GetComponent<Rigidbody2D>();
        ani= GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.x >= open&&!clone)
        if(Time.time>2.0f&&!clone)
        {
            Debug.Log(transform.position.x);
            Debug.Log(Time.time);
            Instantiate(buttP,canvas.transform);
            clone = true;
        }
            
        if (run)
        {
            Debug.Log(rb.velocity);
            //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            //rb.velocity = new Vector2(speed*Input.GetAxis("Horizontal"), 0f);
            ani.SetTrigger("Kairun");
        }
        if(transform.position.x > close)
        {
            SceneChanger();
        }

    }
    public void SceneChanger()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("Opening");//这里最后要改成下一场景名
    }

}
