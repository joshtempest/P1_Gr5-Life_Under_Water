using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;

    public int newScore;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;
    public float lookRadius = 10f;
    public float pickUpRadius = 15f;
    NavMeshAgent agent;
    Transform target;
    public static int enemyScore;
    public int disScore;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyScore = Random.Range(1, 100);
        disScore = enemyScore + 0;
        //Debug.LogFormat("Enemy score: {0}", enemyScore);
    }

    //
    private void FixedUpdate() //Using FixedUpdate here so that the enemy stops moving when the game is paused.
    {
        Vector3 distance = player.position - transform.position; //Finds the distance between enemy and player
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //Gets the angle between the enemy and the player in radians which is then converted
        rb.rotation = angle; //rotates to face the player
        float fdistance = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)); //Finds pythagoras to find the distance to the player as a float
        distance.Normalize(); //Normalizes the distance vector, since we only need the direction
        movement = distance;

        ///<Summary>
        ///Finds the player so that the player score can be used to compare the enemy score with the player score
        /// </Summary>
        
        GameObject Player = GameObject.Find("Player");
        PlayerManagement playerManagement = Player.GetComponent<PlayerManagement>();
        newScore = playerManagement.score;


        if ((enemyScore > newScore) && (fdistance <= lookRadius)) //Moves towards the player if enemy score is higher than player and within it's look radius
        {
            moveCharacter(movement);
        }
    }
    void moveCharacter(Vector2 distance)
    {
        rb.MovePosition((Vector2)transform.position + (distance * moveSpeed * Time.deltaTime));
    }
    private void OnDrawGizmosSelected() //Visualises the look radius to help with making and testing the game
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
