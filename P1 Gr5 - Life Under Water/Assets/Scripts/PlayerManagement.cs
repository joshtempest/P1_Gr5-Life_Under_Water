using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManagement : MonoBehaviour
{
    //Loosly based on "How to make an object follow the mouse in Unity" by Karolio: https://www.youtube.com/watch?v=mF_BB_YsyDk

    //Also includes elements based on "Roll-a-Ball - Displaying Score and Text" by Unity: https://learn.unity.com/tutorial/displaying-score-and-text?uv=2019.4&projectId=5f158f1bedbc2a0020e51f0d#
    //(Maybe unnecessary) Includes elements based on "How to change a Sprite from a script in Unity (with examples)" by gamedevbeginner: https://gamedevbeginner.com/how-to-change-a-sprite-from-a-script-in-unity-with-examples/
    //(???) Includes elements based on "PauseScript" from "Game Project - Find The Portal" by Victor Hejø

    int score = 1;  //This variable keeps track of the score
    public float sizeIncrement; //Determins amount of size increase (Lower number = Bigger increase)
    public float startingSize; //Determines the size that the player object starts at (This should be identical to the scale of the player object itself)
    float playerSize; //Used to Determine the size of the player object
    
    public float speedIncrement; //Determines amount of speed decrease in corralation with the score (Smaller number = Bigger decrease)
    public float startingSpeed; //Determines the speed that the player object starts at
    float speed; //Determines the speed that the player object actually moves at.
    public VariableJoystick variableJoystick; //Calls the VariableJoystick script which is attached to the joystick itself.
    public Rigidbody2D RIGIDBODY; //Calls the rigidbody of the player object.


    public TextMeshProUGUI scoreText; //Text for the score.
    SpriteRenderer sr; //The sprite renderer of the player.
    public Sprite[] playerSprites; //The collection of sprites for the player.
    public int[] sizeLimits; //The collection of sizes that are required to make the sprite change.
    public GameObject gameOverScreen; //The screen that shows when the game is over.
    public int gameLevel; //The build index for the current level.


    [SerializeField] private bool enableDebugging; //This bool is used to enable/disable the printing of debugging values to the console.


    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed; //Sets 'speed' variable to match 'startingSpeed' on start.
        playerSize = startingSize; //Sets 'playerSize' variable to match 'startingSize' on start.

        Time.timeScale = 1; //Makes sure time passes normally and is not stuck when the level is potentially restarted.
        sr = GetComponent<SpriteRenderer>(); //The sprite renderer of the player.
        sr.sprite = playerSprites[0]; //Set the player sprite as the first one.
        SetScoreText(); //Sets up the score from the beginning
        gameOverScreen.SetActive(false); //Sets the game over screen to not be active, so the game can be played.
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= sizeLimits[0] && score <= sizeLimits[1]) //Changes the sprite if the score is between the first and second limit.
        {
            sr.sprite = playerSprites[1];
        }
        else if (score >= sizeLimits[1]) //Changes the sprite if the score passes the second limit.
        {
            sr.sprite = playerSprites[2];
        }
        if (score == 0) //If the score reaches 0, then the game is over and the method is called in order to end it.
        {
            GameOver();
        }
    }

    //This function deals with everything happening when colliding with an object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object") //If collided object have the tag "Object"
        {
            int points = 1; //Local variable that determines how how the score should increase

            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score

            SetScoreText();
            SizeController();
            SpeedController();
            Debugging();
            Destroy(other.gameObject); //Destroy the collided object
        }

        if (other.gameObject.tag == "Object2") //If collided object have the tag "Object"
        {
            int points = 2; //Local variable that determines how how the score should increase

            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score

            SetScoreText();
            SizeController();
            SpeedController();
            Debugging();
            Destroy(other.gameObject); //Destroy the collided object
        }
        if (other.gameObject.tag == "Object3") //If collided object have the tag "Object"
        {
            int points = -1; //Local variable that determines how how the score should increase
            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score
            SetScoreText();
            SizeController();
            SpeedController();
            Debugging();
            Destroy(other.gameObject); //Destroy the collided object
        }
    }
    //This function is responsible for adding the points variable to the score
    int ComputeScore(int a, int b)
    {
        int computedScore = a + b;
        return computedScore;
    }

    //This function is responsible for adjusting the speed of the player object
    void SpeedController()
    {
        speed = startingSpeed - (score / speedIncrement);
    }

    //This function is responsible for adjusting the size of the player object
    void SizeController() 
    {
        playerSize = startingSize + (score / sizeIncrement); // Determines the size of the player object. Devides score by sizeUp to control growth of the player object
        Vector3 sizeVector = new Vector3(playerSize, playerSize, 0); //Creates a new vecter called "sizeVector" which is based on the size variable.
        transform.localScale = sizeVector; //Uses the sizeVector to grow the player object. (Sets scale of object to sizeVector's values)
    }

    //This function is responsible for printing values to the console. It is enabled/disabled by the "enableDebug" bool.
    void Debugging()
    {
        if (enableDebugging == true)
        {
            Debug.Log("Score = " + score); //Prints score to console
            Debug.Log("Player Size = " + playerSize); //Prints playerSize to console
            Debug.Log("Speed = " + speed); //Prints speed to console
        }
    }

    //This function takes the joystick direction and translates it into motion of the player object based on the 'speed' variable.
    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * variableJoystick.Direction + Vector2.right * variableJoystick.Direction;
        RIGIDBODY.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Force);
    }
    //Sets up the score text to show the score
    public void SetScoreText()
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
    //Reloads the game level.
    public void RestartGame()
    {
        SceneManager.LoadScene(gameLevel);
    }
}
