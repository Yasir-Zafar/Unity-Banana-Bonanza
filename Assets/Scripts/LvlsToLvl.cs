using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlsToLvl : MonoBehaviour
{
    public int levelIndex;

    public float delay;

    public Animator BananaBG;
    public Animator BananaSF;
    public Animator BananaF;

    public void GoToTheLevel()
    {
        BananaBG.Play("BananaBGSpawn");
        BananaSF.Play("BananaSFSpawn");
        BananaF.Play("BananaFSpawn");

        StartCoroutine(NextLevelFr());
        
    }

    private IEnumerator NextLevelFr()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelIndex);
    }
}
