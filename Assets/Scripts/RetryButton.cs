using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public int delay;
    public Animator BananaBG;
    public Animator BananaSF;
    public Animator BananaF;
    public Animator MonkeSad;
    public Animator MonkeDance;
    public Animator GokuPose;

    public int levelIndex;
    public void restartLevelWithDelay()
    {
        MonkeSad.gameObject.SetActive(false);
        MonkeDance.gameObject.SetActive(false);
        GokuPose.gameObject.SetActive(false);
        BananaBG.Play("BananaBGSpawn");
        BananaSF.Play("BananaSFSpawn");
        BananaF.Play("BananaFSpawn");
        StartCoroutine(restartLevelFr());
    }

    private IEnumerator restartLevelFr()
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.lose = false;
        GameManager.Instance.SaveStars(levelIndex, 0);
        PersistentManager.Instance.IsRestarting = true;
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
