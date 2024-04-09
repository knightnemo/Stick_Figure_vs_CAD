using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    Button button;
    public GameObject exiting;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnClick()
    {
        exiting.SetActive(true);
        StartCoroutine(Count());
        Application.Quit();
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(5f);
    }
}
