using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.Antlr3.Runtime;

public class SceneControls : MonoBehaviour
{
    //Includes elements based on "Roll-a-Ball - Displaying Score and Text" by Unity: https://learn.unity.com/tutorial/displaying-score-and-text?uv=2019.4&projectId=5f158f1bedbc2a0020e51f0d
    //(???) Includes elements based on "PauseScript" from "Game Project - Find The Portal" by Victor Hejø

    public TextMeshProUGUI scoreText; //Text for the score.
    public GameObject upgradeScreen; //The menu for upgrades.
    public GameObject gameOverScreen; //The screen that shows when the game is over.
    public int sceneNumber; //The build index for the specific scene that needs to be loaded
    public GameObject upgradeMenuButton; //The button that opens and closes the upgrade menu.
    bool isUpgrading; //A boolean that checks whether or not the upgrade menu is open

    public static bool isPaused; //Responsible for pausing/unpausing the game.

    // Start is called before the first frame update
    private void Start()
    {
        //Time.timeScale = 1; //Makes sure time passes normally and is not stuck when the level is potentially restarted.
        gameOverScreen.SetActive(false); //Sets the game over screen to not be active, so the game can be played.
        upgradeScreen.SetActive(false); //Deactivated the upgrade screen on start.
        //isUpgrading = false; //Makes sure the upgrade menu is off.
        upgradeMenuButton.SetActive(true); //Set the upgrade button to be active
    }

    private void FixedUpdate()
    {

    }

    //Loads to a specific scene (Specified in the inspector).
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }

    //Reloads the current level.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Loads the currently active scene (Meant for the game scene)
        ResumeGame(); //Resumes the game to make sure it isn't paused.
    }

    //Sets up the score text to show the score
    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    //Pauses the game through time.timescale and makes the "Game Over Panel" appear.
    public void GameOver()
    {
        gameOverScreen.SetActive(true); //Turns the game over sceen active
        upgradeMenuButton.SetActive(false); //Shuts off the upgrade menu button.
        PauseGame(); //Calls PauseGame() function to pause the game.
    }


    //Switches the upgrade menu on and off.
    public void Upgrades()
    {
        if (isUpgrading) //If you are upgrading, then it will now be turned off
        {
            isUpgrading = false;
            upgradeScreen.SetActive(false);
            ResumeGame(); //Calls function to resumes the game

        }
        else if (!isUpgrading) //If you are not upgrading, then it will now be turned on.
        {
            isUpgrading = true;
            upgradeScreen.SetActive(true);
            PauseGame(); //Calls function to pauses the game
        }
    }


    //Pauses the game when called
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