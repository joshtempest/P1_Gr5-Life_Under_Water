using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    //Loosly based on "How to make an object follow the mouse in Unity" by Karolio: https://www.youtube.com/watch?v=mF_BB_YsyDk
    //(Maybe unnecessary) Includes elements based on "How to change a Sprite from a script in Unity (with examples)" by gamedevbeginner: https://gamedevbeginner.com/how-to-change-a-sprite-from-a-script-in-unity-with-examples/
    //Includes elements based on "HOW TO ACCESS DATA FROM ANOTHER SCRIPT 🎮 | Get Data From Other Scripts In Unity | Unity Tutorial" by Dani Krossing: https://www.youtube.com/watch?v=Y7pp2gzCzUI

    int score = 1;  //This variable keeps track of the score
    public float sizeIncrement; //Determins amount of size increase (Lower number = Bigger increase)
    float playerSize; //Used to Determine the size of the player object.
    public float speed; //Determines the speed of the player object.
    
    public VariableJoystick variableJoystick; //Calls the VariableJoystick script which is attached to the joystick itself.
    public Rigidbody2D RIGIDBODY; //Calls the rigidbody of the player object.

    SpriteRenderer sr; //The sprite renderer of the player.
    public Sprite[] playerSprites; //The collection of sprites for the player.
    public int[] sizeLimits; //The collection of sizes that are required to make the sprite change.
    SceneControls sceneControls; //In order to access the methods in "SceneControls".
    [SerializeField] GameObject sceneController;
    [SerializeField] GameObject upgradeMenu;
    UpgradeManagement upgradeManagement;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); //The sprite renderer of the player.
        sr.sprite = playerSprites[0]; //Set the player sprite as the first one.
        sceneControls = sceneController.GetComponent<SceneControls>();
        sceneControls.SetScoreText(score); //Sets up the score from the beginning
        upgradeManagement = upgradeMenu.GetComponent<UpgradeManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= sizeLimits[0] && score < sizeLimits[1]) //Changes the sprite if the score is between the first and second limit.
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
        else if (score >= sizeLimits[3]) //Changes the sprite if the score passes the fourth limit.
        {
            sr.sprite = playerSprites[4];
        }
        if (score == 0) //If the score reaches 0, then the game is over and the method is called in order to end it.
        {
            sceneControls.GameOver();
        }
    }

    //This function deals with everything happening when colliding with an object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object") //If collided object have the tag "Object"
        {
            int points = 1; //Local variable that determines how how the score should increase

            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score
            sceneControls.SetScoreText(score);

            SizeController();
            Destroy(other.gameObject); //Destroy the collided object
        }

        if (other.gameObject.tag == "Object2") //If collided object have the tag "Object"
        {
            int points = 10; //Local variable that determines how how the score should increase

            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score
            sceneControls.SetScoreText(score);

            SizeController();
            Destroy(other.gameObject); //Destroy the collided object
        }

        if (other.gameObject.tag == "Object3") //If collided object have the tag "Object"
        {
            int points = -1; //Local variable that determines how how the score should increase

            score = ComputeScore(score, points); //Calls the 'ComputeScore' function to calculate the score
            sceneControls.SetScoreText(score);

            SizeController();
            Destroy(other.gameObject); //Destroy the collided object
        }
    }

    //This function is responsible for adding the points variable to the score
    int ComputeScore(int a, int b)
    {
        int computedScore = a + b;

        return computedScore;
    }

    //This function is responsible for adjusting the size of the player object
    void SizeController() 
    {
        playerSize = 1 + (score / sizeIncrement); // Determines the size of the player object. Devides score by sizeUp to control growth of the player object
        Vector3 sizeVector = new Vector3(playerSize, playerSize, 0); //Creates a new vecter called "sizeVector" which is based on the size variable.
        transform.localScale = sizeVector; //Uses the sizeVector to grow the player object. (Sets scale of object to sizeVector's values)
        Debug.Log("Score = " + score); //Prints score to console
        Debug.Log("Player Size = " + playerSize); //Prints playerSize to console
    }

    //This function takes the joystick direction and translates it into motion of the player object.
    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * variableJoystick.Direction + Vector2.right * variableJoystick.Direction;
        RIGIDBODY.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    public void BuyMoreSpeed()
    {
        speed = upgradeManagement.IncreaseSpeed(score,speed);
        Debug.Log(speed);
    }

    public void BuyMoreGrowth()
    {
        sizeIncrement = upgradeManagement.IncreaseGrowth(score,sizeIncrement);
        Debug.Log(sizeIncrement);
    }
}