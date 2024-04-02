using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float flytime = 15.0f;
    float flytimer;
    float livetimer;

    //Renderer
    Renderer rend;
    Material material;
    float colortime = 1.0f;
    Color texturecolor;

    
    public float R, r,d;
    public int tracktype;
    // Start is called before the first frame update
    void Start()
    {
        livetimer = 0f;
        flytimer = flytime;
       
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Shine();

        if (flytimer > 0)
        {
            flytimer -= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Destroy(gameObject);
        }
        if (Bosscontroller.instance == null)
        {
            Destroy(gameObject);
        }
        livetimer+= Time.deltaTime*2;
        if (livetimer > 3)
        {
            R += 0.06f;
        }
        if (Bosscontroller.instance != null)
        {
            transform.position = new Vector3(Bosscontroller.instance.transform.position.x + (R - r) * Mathf.Cos(livetimer) + d * Mathf.Cos(livetimer * (R - r) / r), Bosscontroller.instance.transform.position.y + 16.0f + (R - r) * Mathf.Sin(livetimer + 2 * Mathf.PI * tracktype / 3) - d * Mathf.Sin(livetimer * (R - r) / r + 2 * Mathf.PI * tracktype / 3), 0);
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playercontroller.instance.ChangeHP(-1);
            Debug.Log("Shot!");
            //Destroy(gameObject);
        }
        if (collision.gameObject.tag == "EOL")
        {
            EOLcontroller.instance.HP -= 5;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "shield")
        {
            playercontroller.instance.shieldnum--;
            Destroy(gameObject);
        }
    }

    void Shine()
    {
        colortime += Time.deltaTime * 0.5f;
        if (colortime > 2f)
        {
            colortime = 1.5f;
        }
        //Red
        texturecolor = new Color(2.5f, 1.05f, 1f);
        rend.material.color = texturecolor;

    }
}
