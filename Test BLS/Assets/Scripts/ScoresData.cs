using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresData : MonoBehaviour
{
   
    public static int LoadBestScore()
    {
        int best_score = PlayerPrefs.GetInt("BestScore", 0);
        return best_score;
    }

    public static void SaveBestScore(int best_score)
    {
        PlayerPrefs.SetInt("BestScore", best_score);
    }

    public static int LoadLastScore()
    {
        int last_score = PlayerPrefs.GetInt("LastScore", 0);
        return last_score;
    }

    public static void SaveLastScore(int last_score)
    {
        PlayerPrefs.SetInt("LastScore", last_score);
    }
}
