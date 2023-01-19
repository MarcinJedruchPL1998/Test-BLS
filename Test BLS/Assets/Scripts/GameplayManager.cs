using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] GameObject fadeScreen;

    public InputMenu inputMenu;

    private void Awake()
    {
        fadeScreen.GetComponent<Animator>().enabled = true;
        fadeScreen.GetComponent<Animator>().Play("fade_out");

        inputMenu = new InputMenu();
       
    }

}