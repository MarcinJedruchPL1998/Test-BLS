using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameplayManager : MonoBehaviour
{
    public InputMenu inputMenu;

    [SerializeField] GameObject fadeScreen;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] Text scoresText;
    [SerializeField] Text livesText;

    [SerializeField] PlayerControll playerControll;

    [SerializeField] float time;
    [SerializeField] Text timerText;
    float remainingTime;

    Animator anim;

    bool gameOver;

    GameObject[] enemies;

    private void Awake()
    {
        inputMenu = new InputMenu();
        inputMenu.GameplayInput.Restart.performed += ctx => RestartGame();
        inputMenu.GameplayInput.Exit.performed += ctx => ExitGame();


        //Fade out the screen after loading scene
        anim = fadeScreen.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("fade_out");
    }

    private void OnEnable()
    {
        inputMenu.GameplayInput.Restart.Enable();
        inputMenu.GameplayInput.Exit.Enable();
    }

    private void OnDisable()
    {
        inputMenu.GameplayInput.Restart.Disable();
        inputMenu.GameplayInput.Exit.Disable();
    }

    private void Start()
    {
        remainingTime = time;

        LoadScoresAndLives();
    }


    public void LoadScoresAndLives()
    {
        livesText.text = playerControll.playerLives.ToString();
        scoresText.text = playerControll.playerPoints.ToString();
    }

    private void Update()
    {
        Timer();
    }

    public void Timer() //Set the timer counting down the game time
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        else
        {
            remainingTime = 0;
            gameOver = true;
            ExitGame();
        }

        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver() //Animation Event after destroying player plane, pause the game and show Game Over Screen
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        if(gameOver)
        {
            //If player want to play again, find all enemy planes and destroy them!
            enemies = GameObject.FindGameObjectsWithTag("EnemyPlane");
            foreach(GameObject e in enemies)
            {
                e.GetComponent<EnemyPlane>().DestroyPlane();
            }

            playerControll.ResetPlayer();
            gameOverScreen.SetActive(false);
            gameOver = false;
            Time.timeScale = 1f; //Unpause game
            remainingTime = time; //Reset the timer

            Array.Resize(ref enemies, 0); //Clear found enemy planes from memory
        }
    }

    public void ExitGame()
    {
        if(gameOver)
        {
            Time.timeScale = 1f;

            //Save player points and check if it's higher than best score
            ScoresData.SaveLastScore(playerControll.playerPoints);
            int bestScore = ScoresData.LoadBestScore();
            if(playerControll.playerPoints > bestScore)
            {
                bestScore = playerControll.playerPoints;
                ScoresData.SaveBestScore(bestScore);
            }

            anim.Play("fade_in");
            StartCoroutine(LoadMainMenu());
        }
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

}
