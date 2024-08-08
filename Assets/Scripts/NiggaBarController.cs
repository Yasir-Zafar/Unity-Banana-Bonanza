using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NiggaBarController : MonoBehaviour
{
    public RawImage NiggaBar;
    public Texture one;
    public Texture two;
    public Texture three;
    public Texture four;
    public Texture five;

    void Start()
    {
        NiggaBar.gameObject.SetActive(false);
    }

    void Update()
    {
        switchAccordingly();
    }

    public void switchAccordingly()
    {
        if(GameManager.Instance.niggaBars() == 0)
        {
            NiggaBar.gameObject.SetActive(false);
        }
        else if(GameManager.Instance.niggaBars() == 1)
        {
            NiggaBar.gameObject.SetActive(true);
            NiggaBar.texture = one;
        }
        else if (GameManager.Instance.niggaBars() == 2)
        {
            NiggaBar.gameObject.SetActive(true);
            NiggaBar.texture = two;
        }
        else if (GameManager.Instance.niggaBars() == 3)
        {
            NiggaBar.gameObject.SetActive(true);
            NiggaBar.texture = three;
        }
        else if (GameManager.Instance.niggaBars() == 4)
        {
            NiggaBar.gameObject.SetActive(true);
            NiggaBar.texture = four;
        }
        else if (GameManager.Instance.niggaBars() == 5)
        {
            NiggaBar.gameObject.SetActive(true);
            NiggaBar.texture = five;
        }
    }
}
