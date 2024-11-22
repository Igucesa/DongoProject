using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject tutorialPopUp;



    void Awake()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
        tutorialPopUp.SetActive(false);
    }

    public void TutorialQuestion()
    {
        mainMenu.SetActive(false);
        tutorialPopUp.SetActive(true);
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void BackToMenu()
    {
        credits.SetActive(false);
        tutorialPopUp.SetActive(false);
        mainMenu.SetActive(true);
    }
}
