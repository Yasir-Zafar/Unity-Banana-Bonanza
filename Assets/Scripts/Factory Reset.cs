using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class FactoryReset : MonoBehaviour
{

    public Text messageText;
    public float displayDuration;
    public void factoryReset()
    {
        GameManager.Instance.ResetGameData();
    }

    void Start()
    {
        messageText.gameObject.SetActive(false);
    }

    public void TextForAMoment()
    {
        StartCoroutine(DisplayMessageCoroutine());
    }

    private IEnumerator DisplayMessageCoroutine()
    {
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        messageText.gameObject.SetActive(false);
    }
}
