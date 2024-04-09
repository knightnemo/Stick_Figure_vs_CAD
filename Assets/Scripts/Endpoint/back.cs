using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back : MonoBehaviour
{
    Animator ani;
    Vector2 mousepos;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Mathf.Abs(mousepos.x-transform.position.x)<5.5f && Mathf.Abs(mousepos.y - transform.position.y) < 1.0f)
        {
            ani.SetBool("Selected", true);
            if(Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("Opening");
            }
        }
        else
        {
            ani.SetBool("Selected", false);
        }
    }
}
