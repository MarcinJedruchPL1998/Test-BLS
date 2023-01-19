using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] Text best_score_text;
    [SerializeField] Text last_score_text;

    public InputMenu inputMenu;

    [SerializeField] GameObject fadeScreen;

    void Awake()
    {
        inputMenu = new InputMenu();
        inputMenu.MainMenuInput.AnyButton.performed += ctx => AnyButtonClicked(); //if clicked any key, go to AnyButtonClicked function

        LoadScores();
    }

    private void OnEnable()
    {
        inputMenu.Enable();
    }

    private void OnDisable()
    {
        inputMenu.Disable();
    }

    public void LoadScores() //Load best score and last score from ScoresData
    {
        int bestScore = ScoresData.LoadBestScore();
        int lastScore = ScoresData.LoadLastScore();

        if (bestScore == 0) best_score_text.text = "---";
        else best_score_text.text = bestScore.ToString();

        if (lastScore == 0) last_score_text.text = "---";
        else last_score_text.text = lastScore.ToString();
    }

    public void AnyButtonClicked()
    {
        fadeScreen.GetComponent<Animator>().enabled = true;
        fadeScreen.GetComponent<Animator>().Play("fade_in");

        StartCoroutine(LoadGameplayScene());
    }

    IEnumerator LoadGameplayScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

}
