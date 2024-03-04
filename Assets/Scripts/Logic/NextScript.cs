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
    }

    // Update is called once per frame
    void Update()
    {

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
}
