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

        //Debug.LogFormat("Enemy score: {0}", enemyScore);

        spriteRenderer = GetComponent<SpriteRenderer>();

        EnemySize();
    }

    //
    private void FixedUpdate() //Using FixedUpdate here so that the enemy stops moving when the game is paused.
    {
        player = GameObject.Find("Player").transform;
        Vector3 distance = player.position - transform.position; //Finds the distance between enemy and player
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //Gets the angle between the enemy and the player in radians which is then converted
        float fdistance = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)); //Finds pythagoras to find the distance to the player as a float
        distance.Normalize(); //Normalizes the distance vector, since we only need the direction
        movement = distance;

        ///<Summary>
        ///Finds the player so that the player score can be used to compare the enemy score with the player score
        /// </Summary>
        
        GameObject Player = GameObject.Find("Player");
        PlayerManager playerManagement = Player.GetComponent<PlayerManager>();
        newScore = playerManagement.score;

        if ((enemyScore >= newScore) && (fdistance <= huntRadius)) //Moves towards the player if enemy score is higher than the player and within the huntRadius
        {
            rb.rotation = angle; //rotates to face the player
            sharedBehavior.MoveCharacter(movement, rb, moveSpeed); //Gets the moveCharacter functionality from the SharedBehavior script. 
            sharedBehavior.ObjectFlipper(rb, "unflipX");
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }

        if ((enemyScore < newScore) && (fdistance <= fleeRadius)) //Moves away from the player if enemy score is lower than the player and within the fleeRadius
        {
            rb.rotation = angle; //rotates to face the player
            sharedBehavior.MoveCharacter(-movement, rb, moveSpeed); //Gets the moveCharacter functionality from the SharedBehavior script. Note that "movement" is negative here, this is to make the enemy move away from the player instead of towards.
            sharedBehavior.ObjectFlipper(rb, "flipX"); //Flips the sprite on the "X" axis so that it faces the correct way.
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }

    }
    private void OnDrawGizmosSelected() //Visualises the look radius to help with making and testing the game
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, huntRadius);
    }

    void EnemySize()
    {
        float enemySize = 1 + (enemyScore / sizeIncrement); // Determines the size of the player object. Devides score by sizeUp to control growth of the player object
        Vector3 sizeVector = new Vector3(enemySize, enemySize, 0); //Creates a new vecter called "sizeVector" which is based on the size variable.
        transform.localScale = sizeVector; //Uses the sizeVector to grow the player object. (Sets scale of object to sizeVector's values)
        //SpriteController(); //Runs SpriteController function which is responsible for changing out the player sprites
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