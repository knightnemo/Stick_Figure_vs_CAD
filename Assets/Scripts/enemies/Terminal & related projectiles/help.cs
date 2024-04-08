using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float force = 400.0f;
    public float flytime = 2.0f;
    float flytimer;

    public GameObject enemyC;
    public GameObject enemyduck;
    public GameObject enemyCplus;
    public GameObject enemyCsharp;

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Vector3 v = (playercontroller.instance.transform.position - transform.position).normalized;
        v.y = v.y + 0.5f;
        rigidbody2d.AddForce(v * force);

        flytimer = flytime;

        rend=GetComponent<SpriteRenderer>();
        rend.material.color = new Color(1.2f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(flytimer > 0)
        {
            flytimer-= Time.deltaTime;
        }
        else
        {
            flytimer = flytime;
            Summon();
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Summon();
    }

    void Summon()
    {
        int randomNumber = Random.Range(1, 10);
        if(randomNumber <= 1)
        {
            GameObject C=Instantiate(enemyC,transform.position,Quaternion.identity);
        }
        if(randomNumber>1 && randomNumber <= 6)
        {
            GameObject duck = Instantiate(enemyduck, transform.position, Quaternion.identity);
            
        }
        if(randomNumber>6 && randomNumber<=9)
        {
            
            GameObject Csharp = Instantiate(enemyCsharp, transform.position+ Vector3.up*8.0f, Quaternion.identity);
        }
        if (randomNumber > 9)
        {
            GameObject Cplus = Instantiate(enemyCplus, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
