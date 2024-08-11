using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaCounstShow : MonoBehaviour
{
    public Text bananas;

    // Update is called once per frame
    void Update()
    {
        bananas.text = GameManager.Instance.GetBananas() + "";
    }
}
