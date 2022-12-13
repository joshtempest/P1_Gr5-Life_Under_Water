using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehavior : MonoBehaviour
{
    int playerScore; // Score of the player.
    private Rigidbody2D rb; // Rigidbody2D of the attached object.
    private Vector2 direction; // Direction of the movement.
    public float moveSpeed = 1f; // Sets the movement speed.
    public float huntRadius = 15f; // Sets the radius for when the object should hunt the player.
    public float fleeRadius = 10f; // Sets the radius for when the object should flee the player.
    public int enemyScore; // Score of the enemy object.
    public int enemyScoreRangeStart = 1; // Sets the start value for the range of the enemy score.
    public int enemyScoreRangeEnd = 100; // Sets the end value for the range of the enemy score.
    [SerializeField] float sizeIncrement; // Size increase in relation to the score (Lower number = Bigger increase)

    // Gets all needed scripts.
    public SharedBehavior sharedBehavior;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyScore = Random.Range(enemyScoreRangeStart, enemyScoreRangeEnd);
        EnemySize();
    }

    // Responsible for executing the behavior of an enemy GameObject.
    // Using FixedUpdate here so that the enemy stops moving when the game is paused.
    private void FixedUpdate()
    {
        // Calculates how the enemy should move.
        Vector3 distance = sharedBehavior.CalculateDistance(this.transform.position);
        float angle = sharedBehavior.CalculateAngle(this.transform.position);
        float fdistance = sharedBehavior.CalculateFloatDistance(this.transform.position);
        distance.Normalize();
        direction = distance;

        // Finds the Player GameObject and gets its score.
        GameObject Player = GameObject.Find("Player");
        PlayerManager playerManagement = Player.GetComponent<PlayerManager>();
        playerScore = playerManagement.score;

        // Moves towards the player if enemy score is higher than the player and within the huntRadius
        if ((enemyScore >= playerScore) && (fdistance <= huntRadius))
        {
            rb.rotation = angle;
            sharedBehavior.MoveCharacter(direction, rb, moveSpeed);
            sharedBehavior.ObjectFlipper(rb, "unflipX");
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }
        // Moves away from the player if enemy score is lower than the player and within the fleeRadius
        if ((enemyScore < playerScore) && (fdistance <= fleeRadius))
        {
            rb.rotation = angle;
            sharedBehavior.MoveCharacter(-direction, rb, moveSpeed);
            sharedBehavior.ObjectFlipper(rb, "flipX");
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }
    }
    
    //Visualises the look radius to help with making and testing the game
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, huntRadius);
        Gizmos.DrawWireSphere(transform.position, fleeRadius);
    }

    /// <summary>
    /// This method is used to change the size of the GameObject
    /// </summary>
    void EnemySize()
    {
        float enemySize = 1 + (enemyScore / sizeIncrement); // Determines the size of the enemy object. Devides score by sizeUp to control growth of the enemy object
        Vector3 sizeVector = new Vector3(enemySize, enemySize, 0); //Creates a new vecter called "sizeVector" which is based on the size variable.
        transform.localScale = sizeVector; //Uses the sizeVector to grow the player object. (Sets scale of object to sizeVector's values)
        Debug.Log("Enemy Size = " + enemySize); //Prints enemySize to console
    }

    // This method destroys the enemy if it collieds with an object that has the tag "wall".
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall") //Detect if collided with wall.
        {
            Destroy(gameObject); //Destroy the attached object
        }
    }
}