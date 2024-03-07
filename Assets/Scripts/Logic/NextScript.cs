using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScript : MonoBehaviour
{
    public static NextScript instance{  get; private set; }
    private playercontroller player;
    public GameObject got;
    public float timer;
    private bool sp;
    public bool goNext;

    Renderer rend;
    Color texturecolor;
    float shinetime = 0.27f;
    float shinetimer;
    int litup = 1;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sparkle());
        sp = false;
        goNext = false;
        got.gameObject.SetActive(sp);
        player = playercontroller.instance;

        rend = GetComponent<Renderer>();
        shinetimer = shinetime;
    }

    // Update is called once per frame
    void Update()
    {
        Shine();
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            goNext = true;
        }
    }
    IEnumerator Sparkle()
    {
        while (true)
        {
            sp = !sp;
            got.gameObject.SetActive(sp);
            yield return new WaitForSeconds(timer);
        }

    }

    void Shine()
    {
        if (shinetimer > 0)
        {
            shinetimer -= Time.deltaTime;
        }
        else
        {
            shinetimer = shinetime;
            litup = litup * -1;

        }
        if (litup == 1)
        {
            texturecolor = new Color(1f, 1.3f, 1f);

        }
        if (litup == -1)
        {
            texturecolor = new Color(1f, 1f, 1f);

        }

        rend.material.color = texturecolor;
    }
}
