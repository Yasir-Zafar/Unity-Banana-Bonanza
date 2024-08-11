using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public float delay;

    public Animator BananaBG;
    public Animator BananaSF;
    public Animator BananaF;
    public Animator MonkeSad;
    public Animator MonkeDance;
    public Animator GokuPose;
    public void toLvlsWithDelay()
    {
        MonkeSad.gameObject.SetActive(false);
        MonkeDance.gameObject.SetActive(false);
        GokuPose.gameObject.SetActive(false);
        BananaBG.Play("BananaBGSpawn");
        BananaSF.Play("BananaSFSpawn");
        BananaF.Play("BananaFSpawn");

        StartCoroutine(LvlSlctFr());
    }

    private IEnumerator LvlSlctFr()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level Select");
    }
}
