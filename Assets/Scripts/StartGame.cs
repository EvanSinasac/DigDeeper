using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject instructionPanel;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("maxHealth", 100);
        PlayerPrefs.SetInt("currentHealth", 100);
        PlayerPrefs.SetInt("powerUp", 0);
        PlayerPrefs.SetFloat("powerUpTimer", 0);
        PlayerPrefs.SetFloat("speedPowerUp", 1);
        PlayerPrefs.SetFloat("defencePowerUp", 1);

        if (PlayerPrefs.GetInt("newGame") == 0)
        {
            PlayerPrefs.SetInt("sarahHasActiveQuest", 0);
            PlayerPrefs.SetInt("sarahHasBeenHelped", 0);
            PlayerPrefs.SetInt("sarahAssignedQuest", 0); //quick reset
            PlayerPrefs.SetInt("mitchHasActiveQuest", 0);
            PlayerPrefs.SetInt("mitchHasBeenHelpred", 0);
            PlayerPrefs.SetInt("mitchAssignedQuest", 0);
            PlayerPrefs.SetInt("karlHasActiveQuest", 0);
            PlayerPrefs.SetInt("karlHasBeenHelped", 0);
            PlayerPrefs.SetInt("karlAssignedQuest", 0);
            PlayerPrefs.SetInt("clancyHasActiveQuest", 0);
            PlayerPrefs.SetInt("clancyHasBeenHelped", 0);
            PlayerPrefs.SetInt("clancyAssignedQuest", 0);

            PlayerPrefs.SetInt("newGame", 1);
            PlayerPrefs.Save();
        }

        mainMenuPanel = GameObject.FindGameObjectWithTag("MainMenu");
        instructionPanel = GameObject.FindGameObjectWithTag("InstructionsMenu");

        hideInstructions();

    }

    public void TownLoad()
    {
        //Debug.Log("Clicked Button");
        SceneManager.LoadScene(5); //Load Town //changed it to inside the house, otherwise players will technically never know they can go there
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    void showInstructions ()
    {
        instructionPanel.SetActive(true);
    }

    void hideInstructions ()
    {
        instructionPanel.SetActive(false);
    }

    void showMainMenu ()
    {
        mainMenuPanel.SetActive(true);
    }

    void hideMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void instructionsButton ()
    {
        hideMainMenu();
        showInstructions();
    }

    public void returnButton ()
    {
        hideInstructions();
        showMainMenu();
    }
}
