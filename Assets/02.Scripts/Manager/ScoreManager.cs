using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int totalKillCount = 0;
    public int totalDamagedCount = 0;
    public int totalGoldCount = 0;
    public int totalItemCount = 0;

    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ScoreManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object ScoreManager");
                }
            }

            return instance;
        }
    }

    public void SetTotalKillCount()
    {
        totalKillCount += 1;
    }

    public void SetTotalDamagedCount()
    {
        totalDamagedCount += 1;
    }

    public void SetTotalGold()
    {
        totalGoldCount += 10;
    }

    public void SetTotalItem()
    {
        totalItemCount += 1;
    }
}
