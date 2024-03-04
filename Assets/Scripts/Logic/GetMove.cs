using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMove : MonoBehaviour
{
    private playercontroller player;
    public GameObject got;
    public float timer;
    private bool sp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sparkle());
        sp = false;
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
            player.canTeleport = true;
            gameObject.SetActive(false);
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
