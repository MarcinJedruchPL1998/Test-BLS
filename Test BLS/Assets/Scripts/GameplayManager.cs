using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] GameObject fadeScreen;

    Animator anim;

    private void Awake()
    {
        anim = fadeScreen.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("fade_out");
    }

}
