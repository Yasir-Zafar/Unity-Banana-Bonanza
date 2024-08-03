using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarControllerScript : MonoBehaviour
{
    public RawImage L;
    public RawImage M;
    public RawImage R;

    public Texture LN;
    public Texture MN;
    public Texture RN;
    public Texture LY;
    public Texture MY;
    public Texture RY;

    public int starsCount;
    public int levelIndex;
    public float maxGems;

    void Start()
    {
        starsCount = 0;
    }

    void Update()
    {
        updateL();
        updateM();
        updateR();

        if (R.texture == RY)
        {
            starsCount = 3;
        }
        else if (M.texture == MY)
        {
            starsCount = 2;
        }
        else if(L.texture == LY)
        {
            starsCount = 1;
        }

        GameManager.Instance.SaveStars(levelIndex, starsCount);
    }

    //1st star
    void updateL()
    {
        L.texture = LY;
    }

    //2nd star
    void updateM()
    {
        if (GemManager.Instance.GetTotalGems() >= (maxGems/(2.0f)))
        {
            M.texture = MY;
        }
        else
        {
            M.texture = MN;
        }
    }

    //3rd star
    void updateR()
    {
        if (GemManager.Instance.GetTotalGems() == maxGems)
        {
            R.texture = RY;
        }
        else
        {
            R.texture = RN;
        }
    }
}
