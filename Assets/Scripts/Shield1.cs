using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shield1 : MonoBehaviour
{
    public Button button2;
    private Button button1;
    public AudioClip ring;
    private AudioSource audioSource;
    public TextMeshProUGUI txt;

    private void Start()
    {
        txt.gameObject.SetActive(false);

        if(GameManager.Instance.ShieldTimeUp1 == true)
        {
            button1 = GetComponent<Button>();
            button1.gameObject.SetActive(false);
        }

        if (GameManager.Instance.GetBananas() >= 10)
        {
            button1 = GetComponent<Button>();
            button1.interactable = true;
        }
    }

    public void UnlockShield1()
    {
        button1 = GetComponent<Button>();
        GameManager.Instance.ShieldTimeUp1 = true;       
        GameManager.Instance.SaveShieldTimes();
        GameManager.Instance.SaveBananasCollected(-10);
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
        button1.gameObject.SetActive(false);
    }
}
