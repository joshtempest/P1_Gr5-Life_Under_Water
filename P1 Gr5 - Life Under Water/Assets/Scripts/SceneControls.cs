using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneControls : MonoBehaviour
{
    //Includes elements based on "Roll-a-Ball - Displaying Score and Text" by Unity: https://learn.unity.com/tutorial/displaying-score-and-text?uv=2019.4&projectId=5f158f1bedbc2a0020e51f0d
    //(???) Includes elements based on "PauseScript" from "Game Project - Find The Portal" by Victor Hejø

    public TextMeshProUGUI scoreText; //Text for the score.
    public GameObject upgradeScreen; //The menu for upgrades.
    public GameObject gameOverScreen; //The screen that shows when the game is over.
    public int sceneNumber; //The build index for the specific scene that needs to be loaded
    bool upgrading; //A boolean that checks whether or not the upgrade menu is open
    public GameObject upgradeMenuButton; //The button that opens and closes the upgrade menu.
    
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1; //Makes sure time passes normally and is not stuck when the level is potentially restarted.
        gameOverScreen.SetActive(false); //Sets the game over screen to not be active, so the game can be played.
        upgrading = false;//Makes sure the upgrade menu is off.
        upgradeMenuButton.SetActive(true); //Set the upgrade button to be active
    }

    private void FixedUpdate()
    {
        //Switches the upgrade menu on and off.
        if (upgrading) //If you are upgrading, then the upgrade menu is active
        {
            upgradeScreen.SetActive(true);
        }
        else if (!upgrading) //If you are not upgrading, then the upgrade menu is not active.
        {
            upgradeScreen.SetActive(false);
        }
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
    }

    //Sets up the score text to show the score
    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    //Pauses the game through time.timescale and makes the "Game Over Panel" appear.
    public void GameOver()
    {
        Time.timeScale = 0; //Stops the game
        gameOverScreen.SetActive(true); //Turns the game over sceen active
        upgradeMenuButton.SetActive(false); //Shuts off the upgrade menu button.
    }

    //Switches the boolean between true and false in order to turn the menu on and off.
    public void Upgrades()
    {
        if (upgrading) //If you are upgrading, then it will now be turned off
        {
            upgrading = false;
        }
        else if (!upgrading) //If you are not upgrading, then it will now be turned on.
        {
            upgrading = true;
        }
    }
}