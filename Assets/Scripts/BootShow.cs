using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BootShow : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public GameObject boot;


    void Start()
    {
        txt.gameObject.SetActive(false);
    }

    void Update()
    {
        if(boot.gameObject.active == false)
        {
            StartCoroutine(ShowText());
        }
    }

    private IEnumerator ShowText()
    {
        txt.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        txt.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
