using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.Antlr3.Runtime;

public class MenuManager : MonoBehaviour
{
    //Includes elements based on "Roll-a-Ball - Displaying Score and Text" by Unity: https://learn.unity.com/tutorial/displaying-score-and-text?uv=2019.4&projectId=5f158f1bedbc2a0020e51f0d
    //(???) Includes elements based on "PauseScript" from "Game Project - Find The Portal" by Victor Hejø

    public TextMeshProUGUI scoreText; //Text for the score.
    public GameObject upgradeScreen; //The menu for upgrades.
    public GameObject gameOverScreen; //The screen that shows when the game is over.
    public GameObject upgradeMenuButton; //The button that opens and closes the upgrade menu.
    bool isUpgrading; //A boolean that checks whether or not the upgrade menu is open
    public GameObject pauseMenuButton; //Button for the pause button
    public GameObject pauseMenu; //The pause menu

    public GameObject achievementButton; //AchievementMenu Button

    public GameObject infoMenu;
    bool isInforming;

    public static bool isPaused; //Responsible for pausing/unpausing the game.

    private void Start()
    {
        //Time.timeScale = 1; //Makes sure time passes normally and is not stuck when the level is potentially restarted.
        //gameOverScreen.SetActive(false); //Sets the game over screen to not be active, so the game can be played.
        //upgradeScreen.SetActive(false); //Deactivated the upgrade screen on start.
        //pauseMenu.SetActive(false);
        //isUpgrading = false; //Makes sure the upgrade menu is off.
        //upgradeMenuButton.SetActive(true); //Set the upgrade button to be active
        //pauseMenuButton.SetActive(true);
    }

    /// <summary>
    /// Loads to a specific scene (Specified in the inspector).
    /// </summary>
    /// <param name="sceneNumber"></param>
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Reloads the current level when called.
    /// </summary>
    public void RestartGame()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex); //Loads the currently active scene (Meant for the game scene)
        gameOverScreen.SetActive(false);
        pauseMenuButton.SetActive(false);
        ResumeGame(); //Resumes the game to make sure it isn't paused.
    }

    /// <summary>
    /// Sets up the score text to show the score
    /// </summary>
    /// <param name="score"></param>
    public void SetScoreText(int score)
    {
        scoreText.text = "Vægt:\n" + score.ToString() + " kg";
    }

    /// <summary>
    /// Pauses the game through time.timescale and makes the "Game Over Panel" appear.
    /// </summary>
    public void GameOver()
    {
        gameOverScreen.SetActive(true); //Turns the game over sceen active
        upgradeMenuButton.SetActive(false); //Shuts off the upgrade menu button.
        pauseMenuButton.SetActive(false); //Shuts off the pause menu button.
        PauseGame(); //Calls PauseGame() function to pause the game.
    }


    /// <summary>
    /// Opens and closes the upgrade menu when called.
    /// </summary>
    public void UpgradesMenu()
    {
        if (isUpgrading) //If you are upgrading, then it will now be turned off
        {
            isUpgrading = false;
            upgradeScreen.SetActive(false);
            pauseMenuButton.SetActive(true);
            upgradeMenuButton.SetActive(true);
            scoreText.gameObject.SetActive(true);
            //achievementButton.gameObject.SetActive(true);
            ResumeGame(); //Calls function to resumes the game

        }
        else if (!isUpgrading) //If you are not upgrading, then it will now be turned on.
        {
            isUpgrading = true;
            upgradeScreen.SetActive(true);
            pauseMenuButton.SetActive(false);
            upgradeMenuButton.SetActive(false);
            scoreText.gameObject.SetActive(false);
            //achievementButton.gameObject.SetActive(false);
            PauseGame(); //Calls function to pauses the game
        }
    }

    /// <summary>
    /// Opens and closes the pause menu when called.
    /// </summary>
    public void PauseMenu()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            pauseMenuButton.SetActive(true);
            upgradeMenuButton.SetActive(true);
            scoreText.gameObject.SetActive(true);
            //achievementButton.gameObject.SetActive(true);
            ResumeGame();
            
        }
        else if (!isPaused)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            pauseMenuButton.SetActive(false);
            upgradeMenuButton.SetActive(false);
            scoreText.gameObject.SetActive(false);
            //achievementButton.gameObject.SetActive(false);
            PauseGame();

        }
    }

    /// <summary>
    /// Opens and closes the information menu when called.
    /// </summary>
    public void InfoMenu()
    {
        if (isInforming)
        {
            isInforming = false;
            infoMenu.SetActive(false);
            ResumeGame(); //Calls function to resumes the game

        }
        else if (!isUpgrading) //If you are not upgrading, then it will now be turned on.
        {
            isInforming = true;
            infoMenu.SetActive(true);
            PauseGame(); //Calls function to pauses the game
        }
    }

    /// <summary>
    /// Pauses the game when called
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f; //Stops the game
        isPaused = true;
        Debug.Log("Paused"); //Prints to console that the game is paused
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; //Resumes the game
        isPaused = false;
        Debug.Log("Resumed"); //Prints to console that the game has resumed
    }
}