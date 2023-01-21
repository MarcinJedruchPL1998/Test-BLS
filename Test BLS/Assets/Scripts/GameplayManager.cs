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

    Animator anim;

    bool gameOver;

    public GameObject[] enemies;

    private void Awake()
    {
        inputMenu = new InputMenu();
        inputMenu.GameplayInput.Restart.performed += ctx => RestartGame();
        inputMenu.GameplayInput.Exit.performed += ctx => ExitGame();

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
        LoadScoresAndLives();
        Timer();
    }


    public void LoadScoresAndLives()
    {
        livesText.text = playerControll.playerLives.ToString();
        scoresText.text = playerControll.playerPoints.ToString();
    }

    public void Timer()
    {

    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        if(gameOver)
        {
            enemies = GameObject.FindGameObjectsWithTag("EnemyPlane");
            foreach(GameObject e in enemies)
            {
                e.GetComponent<EnemyPlane>().DestroyPlane();
            }

            playerControll.ResetPlayer();
            gameOverScreen.SetActive(false);
            gameOver = false;
            Time.timeScale = 1f;

            Array.Resize(ref enemies, 0);
        }
    }

    public void ExitGame()
    {
        if(gameOver)
        {
            Time.timeScale = 1f;
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
