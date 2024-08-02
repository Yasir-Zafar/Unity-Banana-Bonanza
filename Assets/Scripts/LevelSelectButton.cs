using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public void toLvls()
    {
        SceneManager.LoadScene("Level Select");
    }
}
