using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield1 : MonoBehaviour
{
    public Button button2;
    private Button button1;
    public AudioClip ring;
    private AudioSource audioSource;

    private void Start()
    {
        if (GameManager.Instance.GetBananas() >= 10)
        {
            button1 = GetComponent<Button>();
            button1.interactable = true;
        }
    }

    public void UnlockShield1()
    {
        button1 = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ring;
        audioSource.Play();
        GameManager.Instance.ShieldTimeUp1 = true;            
        GameManager.Instance.SaveBananasCollected(GameManager.Instance.bananas - 10);
        StartCoroutine(somebodyShootThatBastard());
    }

    private IEnumerator somebodyShootThatBastard()
    {
        yield return new WaitForSeconds(1);
        button1.gameObject.SetActive(false);
    }
}
