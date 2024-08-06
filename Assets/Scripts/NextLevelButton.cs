using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class NextLevelButton : MonoBehaviour
{
    public float delay;

    public Animator BananaBG;
    public Animator BananaSF;
    public Animator BananaF;
    public Animator MonkeDance;


    public void EndGame()
    {
        MonkeDance.gameObject.SetActive(false);
        BananaBG.Play("BananaBGSpawn");
        BananaSF.Play("BananaSFSpawn");
        BananaF.Play("BananaFSpawn");

        StartCoroutine(LoadLevelAfterDelay());
    }
    public IEnumerator LoadLevelAfterDelay()
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        yield return new WaitForSeconds(delay);         //delay
        if (sceneController != null)
        {

            sceneController.LoadNextLevel();
        }
    }
}
