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
    Transform player;

    int newScore;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;
    public float huntRadius = 15f;
    public float fleeRadius = 10f;
    //public float pickUpRadius = 15f;
    NavMeshAgent agent;
    Transform target;
    public int enemyScore;
    public int enemyScoreRangeStart = 1;
    public int enemyScoreRangeEnd = 100;
    [HideInInspector] public int disScore;
    private SpriteRenderer spriteRenderer;
    [SerializeField] float sizeIncrement;

    // Gets all scripts needed.
    public SharedBehavior sharedBehavior;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyScore = Random.Range(enemyScoreRangeStart, enemyScoreRangeEnd);
        disScore = enemyScore + 0;
        EnemySize();
    }

    // Using FixedUpdate here so that the enemy stops moving when the game is paused.
    // Responsible for the behavior of an enemy GameObject.
    private void FixedUpdate()
    {
        player = GameObject.Find("Player").transform;
        Vector3 distance = player.position - transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        float fdistance = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2));
        distance.Normalize();
        movement = distance;
        
        GameObject Player = GameObject.Find("Player");
        PlayerManager playerManagement = Player.GetComponent<PlayerManager>();
        newScore = playerManagement.score;

        // Moves towards the player if enemy score is higher than the player and within the huntRadius
        if ((enemyScore >= newScore) && (fdistance <= huntRadius))
        {
            rb.rotation = angle;
            sharedBehavior.MoveCharacter(movement, rb, moveSpeed);
            sharedBehavior.ObjectFlipper(rb, "unflipX");
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }
        // Moves away from the player if enemy score is lower than the player and within the fleeRadius
        if ((enemyScore < newScore) && (fdistance <= fleeRadius))
        {
            rb.rotation = angle;
            sharedBehavior.MoveCharacter(-movement, rb, moveSpeed);
            sharedBehavior.ObjectFlipper(rb, "flipX");
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }
    }
    private void OnDrawGizmosSelected() //Visualises the look radius to help with making and testing the game
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, huntRadius);
    }

    // This method is used to change the size of the GameObject
    void EnemySize()
    {
        float enemySize = 1 + (enemyScore / sizeIncrement); // Determines the size of the enemy object. Devides score by sizeUp to control growth of the enemy object
        Vector3 sizeVector = new Vector3(enemySize, enemySize, 0); //Creates a new vecter called "sizeVector" which is based on the size variable.
        transform.localScale = sizeVector; //Uses the sizeVector to grow the player object. (Sets scale of object to sizeVector's values)
        Debug.Log("Enemy Size = " + enemySize); //Prints enemySize to console
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall") //Detect if collided with wall.
        {
            Destroy(gameObject); //Destroy the attached object
        }
    }
}