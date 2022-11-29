using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class PreyBehavior : MonoBehaviour
{
    Transform player;

    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;
    public float lookRadius = 10f;
    //public float pickUpRadius = 15f;
    NavMeshAgent agent;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    //
    private void FixedUpdate() //Using FixedUpdate here so that the prey stops moving when the game is paused.
    {
        player = GameObject.Find("Player").transform;
        Vector3 distance = player.position - transform.position; //Finds the distance between prey and player
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //Gets the angle between the prey and the player in radians which is then converted
        rb.rotation = angle; //rotates to face the player
        //transform.rotation = Quaternion.Inverse(target.rotation); //Inverts the rotation so that the prey faces away from the player.
        float fdistance = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)); //Finds pythagoras to find the distance to the player as a float
        distance.Normalize(); //Normalizes the distance vector, since we only need the direction
        movement = -1 * distance; //Makes the prey move away from the player.

        ///<Summary>
        ///Finds the player so that the player score can be used to compare the prey score with the player score
        /// </Summary>
        
        GameObject Player = GameObject.Find("Player");
        PlayerManager playerManagement = Player.GetComponent<PlayerManager>();

        if (fdistance <= lookRadius) //Moves away from the player if within its look radius
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
