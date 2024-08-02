using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance { get; private set; }
    private int totalGems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectGem()
    {
        totalGems++;
        Debug.Log("Total Gems Collected: " + totalGems);
    }

    public void ResetGemCount()
    {
        totalGems = 0;
    }

    public int GetTotalGems()
    {
        return totalGems;
    }
}
