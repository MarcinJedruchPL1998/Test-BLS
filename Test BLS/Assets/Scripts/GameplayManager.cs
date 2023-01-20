using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] GameObject fadeScreen;

    [SerializeField] Text scoresText;
    [SerializeField] Text livesText;

    int playerLives = 3;
    int playerPoints;

    Animator anim;

    private void Awake()
    {
        anim = fadeScreen.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("fade_out");
    }

    private void Start()
    {
        LoadScoresAndLives();
    }


    public void LoadScoresAndLives()
    {
        int last_scores = ScoresData.LoadLastScore();

        livesText.text = playerLives.ToString();
        scoresText.text = last_scores.ToString(); 
    }

    public void AddPoint()
    {
        playerPoints += 10;
        scoresText.text = playerPoints.ToString();
    }

    public void RemoveLive()
    {
        if(playerLives > 0)
        {
            playerLives--;
            livesText.text = playerLives.ToString();
        }

        else
        {

        }
    }
}
