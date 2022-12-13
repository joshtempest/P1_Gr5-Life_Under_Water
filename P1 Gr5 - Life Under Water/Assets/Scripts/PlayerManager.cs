using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerManager : MonoBehaviour
{
    //Loosly based on "How to make an object follow the mouse in Unity" by Karolio: https://www.youtube.com/watch?v=mF_BB_YsyDk
    //(Maybe unnecessary) Includes elements based on "How to change a Sprite from a script in Unity (with examples)" by gamedevbeginner: https://gamedevbeginner.com/how-to-change-a-sprite-from-a-script-in-unity-with-examples/
    //Includes elements based on "HOW TO ACCESS DATA FROM ANOTHER SCRIPT 🎮 | Get Data From Other Scripts In Unity | Unity Tutorial" by Dani Krossing: https://www.youtube.com/watch?v=Y7pp2gzCzUI

    public int score = 1;  // Score of the player.
    public int sizeIncrement; // Size increase in relation to the score (Lower number = Bigger increase)
    float playerSize; // Determines the size of the player object.
    float cameraSize; // Determines the size of the camera.
    public float speed; // Determines the speed of the player object.
    public int growthMultiplier; // Determines the growth multiplier for the score.
    public VariableJoystick variableJoystick; // Calls the VariableJoystick script which is attached to the joystick itself.
    Rigidbody2D rb; // Rigidbody of the player object.
    SpriteRenderer sr; // The sprite renderer of the player.
    public Sprite[] playerSprites; // The collection of sprites for the player.
    public int[] sizeLimits; // The collection of sizes that are required to make the sprite change.
    public GameObject player;

    // Gets all needed scripts.
    EnemyBehavior enemyBehavior;
    [SerializeField] GameObject menuController; // The game object that has the script "MenuManager".
    MenuManager menuManager; // In order to access the methods in "MenuManager".
    [SerializeField] GameObject upgradeMenu; // The GameObject of the Upgrade Menu.
    UpgradeManagement upgradeManagement; // In order to access the methods in "UpgradeManagement".

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); //The sprite renderer of the player.
        sr.sprite = playerSprites[0]; //Set the player sprite as the first one.
        menuManager = menuController.GetComponent<MenuManager>(); //Gets the script "SceneControls" from the scene controller.
        menuManager.SetScoreText(score); //Sets up the score from the beginning
        upgradeManagement = upgradeMenu.GetComponent<UpgradeManagement>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This function deals with everything happening when the player collides with an object.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If collided object have the tag "Object".
        if (other.gameObject.tag == "Object")
        {
            int points = 1; // Local variable that determines how how the score should increase

            score = ComputeScore(score, points); // Calls the 'ComputeScore' function to calculate the score
            menuManager.SetScoreText(score); // Calls the 'SetScoreText' function from "SceneControls" to set the score text.
            SizeController(); // Runs "SizeController" to control the size.
            Destroy(other.gameObject); // Destroy the collided object
        }
        // If collided object have the tag "Object2".
        if (other.gameObject.tag == "Object2")
        {
            int points = 4; // Local variable that determines how how the score should increase

            score = ComputeScore(score, points);// Calls the 'ComputeScore' function to calculate the score
            menuManager.SetScoreText(score);// Calls the 'SetScoreText' function from "MenuManager" to set the score text.
            SizeController(); // Runs "SizeController" to control the size.
            Destroy(other.gameObject); // Destroy the collided object
        }
        // If collided object have the tag "Object3".
        if (other.gameObject.tag == "Object3")
        {
            int points = -1; // Local variable that determines how how the score should increase

            score = ComputeScore(score, points); // Calls the 'ComputeScore' function to calculate the score
            menuManager.SetScoreText(score); // Calls the 'SetScoreText' function from "MenuManager" to set the score text.
            SizeController(); // Runs "SizeController" to control the size.
            Destroy(other.gameObject); // Destroy the collided object.
        }
        // If collided object have the tag "Prey"
        if (other.gameObject.tag == "Prey")
        {
            int points = 10; //Local variable that determines how how the score should increase

            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score
            menuManager.SetScoreText(score); //Calls the 'SetScoreText' function from "MenuManager" to set the score text.
            SizeController(); //Runs "SizeController" to control the size.
            Destroy(other.gameObject); //Destroy the collided object
        }
        // If collided object have the tag "Enemy".
        if (other.gameObject.tag == "Enemy")
        {
            enemyBehavior = other.gameObject.GetComponent<EnemyBehavior>(); // Allows this script to access code from the collided gameobject.
            int newEnemyScore = enemyBehavior.enemyScore;
            if (newEnemyScore >= score) 
            {
                int points = -newEnemyScore; // Local variable that determines how much the score should increase
                score = ComputeScore(score, points); // Calls the 'ComputeScore' function to calculate the score
                menuManager.SetScoreText(score);
                SizeController();
                Destroy(other.gameObject); // Destroy the collided object
            }
            else
            {
                int point = newEnemyScore;
                score = ComputeScore(score, point);
                menuManager.SetScoreText(score);
                SizeController();
                Destroy(other.gameObject);
            } 
        }
    }

    /// <summary>
    /// This method is responsible for calculating the new score based on the given parameters.
    /// </summary>
    /// <param name="scoreArgument"></param>
    /// <param name="pointArgument"></param>
    /// <returns></returns>
    int ComputeScore(int scoreArgument, int pointArgument)
    {
        int computedScore = scoreArgument + (pointArgument * growthMultiplier);

        return computedScore;
    }

    /// <summary>
    /// This function is responsible for adjusting the size of the player object
    /// </summary>
    void SizeController() 
    {
        playerSize = 3 + ((float)score / (float)sizeIncrement); // Determines the size of the player object. Devides score by sizeUp to control growth of the player object
        cameraSize = score / sizeIncrement; // Determines the size of the camera.
        Vector3 sizeVector = new Vector3(playerSize, playerSize, 0); //Creates a new vecter called "sizeVector" which is based on the size variable.
        transform.localScale = sizeVector; //Uses the sizeVector to grow the player object. (Sets scale of object to sizeVector's values)

        Camera.main.orthographicSize = 5 + cameraSize; //Used the cameraSize variable to set the orthographicSize of the camera so that it follows the size of the player object.

        Debug.Log("Score = " + score); //Prints score to console
        Debug.Log("Player Size = " + playerSize); //Prints playerSize to console
        SpriteController(); //Runs SpriteController function which is responsible for changing out the player sprites
    }

    // This function takes the joystick direction and translates it into motion of the player object.
    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * variableJoystick.Direction + Vector2.right * variableJoystick.Direction;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    /// <summary>
    /// This is the function that buys increased speed when pushing the button in the upgrade menu.
    /// </summary>
    public void BuyMoreSpeed()
    {
        float[] info = new float[2]; //An array to get both the new score and the new speed.
        info = upgradeManagement.IncreaseSpeed(score,speed); //Calls "IncreaseSpeed" and contains the info in the array.
        score = (int) info[0]; //Changes the score to the new value.
        menuManager.SetScoreText(score); //Sets the score text.
        SizeController(); //Making sure the size of the player matches the new score.
        speed = info[1]; //Changes the speed to the new value.
    }

    /// <summary>
    /// This is the function that buys increased growth when pushing the button in the upgrade menu.
    /// </summary>
    public void BuyMoreGrowth()
    {
        int[] info; //An array to get both the new score and the new size increment.
        info = upgradeManagement.IncreaseGrowth(score,sizeIncrement, growthMultiplier); //Calls "IncreaseGrowth" and contains the info in the array.
        score = info[0]; //Changes the score to the new value.
        menuManager.SetScoreText(score); //Sets the score text.
        SizeController(); //Making sure the size of the player matches the new score.
        sizeIncrement = info[1]; //Changes the size increment to the new value.
        growthMultiplier = info[2];
    }

    /// <summary>
    /// This function controls when the player sprite should change in relation to the score.
    /// </summary>
    private void SpriteController()
    {
        if (score < sizeLimits[0]) //Changes the sprite if score is under the first limit
        {
            sr.sprite = playerSprites[0];
        }
        else if (score >= sizeLimits[0] && score < sizeLimits[1]) //Changes the sprite if the score is between the first and second limit.
        {
            sr.sprite = playerSprites[1];                      
        }
        else if (score >= sizeLimits[1] && score < sizeLimits[2]) //Changes the sprite if the score is between the second and third limit.
        {
            sr.sprite = playerSprites[2];            
        }
        else if (score >= sizeLimits[2] && score < sizeLimits[3]) ///Changes the sprite if the score is between the third and fourth limit.
        {
            sr.sprite = playerSprites[3];            
        }
        else if (score >= sizeLimits[3] && score < sizeLimits[4]) ///Changes the sprite if the score is between the fourth and fifth limit.
        {
            sr.sprite = playerSprites[4];            
        }
        else if (score >= sizeLimits[4]) //Changes the sprite if the score passes the fifth limit.
        {
            EndScene.totalScore = score;
            menuManager.LoadScene(2);
        }
            if (score <= 0) //If the score reaches or is below 0, then the game is over and the method is called in order to end it.
        {
            menuManager.GameOver();
        }
    }
}