using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    public Animator button;
    public string AnimationName;
    public void output()
    {
        Debug.Log("Clicked");
        button.Play(AnimationName);
    }
}
