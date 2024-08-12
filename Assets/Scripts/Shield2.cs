using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shield2 : MonoBehaviour
{
    public Button button2;
    public AudioClip ring;
    private AudioSource audioSource;
    public TextMeshProUGUI txt;

    private void Start()
    {
        txt.gameObject.SetActive(false);

        if (GameManager.Instance.ShieldTimeUp2 == true)
        {
            button2.gameObject.SetActive(false);
        }

        if (GameManager.Instance.ShieldTimeUp1 == true && GameManager.Instance.GetBananas() >= 15)
        {
            button2 = GetComponent<Button>();
            button2.interactable = true;
        }
    }

    public void UnlockShield2()
    {
        button2 = GetComponent<Button>();
        GameManager.Instance.ShieldTimeUp2 = true;
        GameManager.Instance.SaveShieldTimes();
        GameManager.Instance.SaveBananasCollected(-15);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ring;
        audioSource.Play();
        txt.gameObject.SetActive(true);
        StartCoroutine(somebodyShootThatBastard());
    }

    private IEnumerator somebodyShootThatBastard()
    {
        yield return new WaitForSeconds(0.9f);
        txt.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }
}
