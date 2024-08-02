using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsView : MonoBehaviour
{
    public int[] stars = new int[9];

    public RawImage[] L = new RawImage[9];
    public RawImage[] M = new RawImage[9];
    public RawImage[] R = new RawImage[9];

    public Texture LY;
    public Texture MY;
    public Texture RY;
    public Texture LN;
    public Texture MN;
    public Texture RN;

    void Start()
    {
        //load
        for (int i = 0; i < 9; i++)
        {
            stars[i] = GameManager.Instance.GetStars(i);
            if (stars[i] == 0)
            {
                L[i].enabled = false;
                M[i].enabled = false;
                R[i].enabled = false;
            }
            else if (stars[i] == 1)
            {
                L[i].texture = LY;
                M[i].texture = MN;
                R[i].texture = RN;
            }
            else if (stars[i] == 2)
            {
                L[i].texture = LY;
                M[i].texture = MY;
                R[i].texture = RN;
            }
            else if (stars[i] == 3)
            {
                L[i].texture = LY;
                M[i].texture = MY;
                R[i].texture = RY;
            }
        }

    }   
}
