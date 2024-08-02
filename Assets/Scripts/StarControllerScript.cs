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

    public int M_Limit;
    public int R_Limit;

    public Text swipes;

    public int starsCount;
    public int levelIndex;

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
        if (int.Parse(swipes.text) < M_Limit)
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
        if (int.Parse(swipes.text) < R_Limit)
        {
            R.texture = RY;
        }
        else
        {
            R.texture = RN;
        }
    }
}
