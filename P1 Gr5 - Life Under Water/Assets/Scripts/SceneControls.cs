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
    public GameObject gameOverScreen; //The screen that shows when the game is over.
    public int sceneNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //Makes sure time passes normally and is not stuck when the level is potentially restarted.
        gameOverScreen.SetActive(false); //Sets the game over screen to not be active, so the game can be played.
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }

    //Reloads the game level.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Sets up the score text to show the score
    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    //Pauses the game through time.timescale and makes the "Game Over Panel" appear.
    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
        gameOverScreen.SetActive(true);
    }
}