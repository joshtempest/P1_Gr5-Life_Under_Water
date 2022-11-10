using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public Transform player;
    
    public int newScore;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;
    public float lookRadius = 10f;
    NavMeshAgent agent;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    //
    private void FixedUpdate() //Using FixedUpdate here so that the enemy stops moving when the game is paused.
    {
        Vector3 distance = player.position - transform.position; //Finds the distance between enemy and player
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //Gets the angle between the enemy and the player in radians which is then converted
        rb.rotation = angle; //rotates to face the player
        float fdistance = Mathf.Sqrt((Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)));
        distance.Normalize();
        Debug.LogFormat("fdistance is {0}",fdistance);
        movement = distance;


        if (fdistance <= lookRadius)
        {
            moveCharacter(movement);
        }

        
        GameObject Player = GameObject.Find("Player");
        PlayerManagement playerManagement = Player.GetComponent<PlayerManagement>();
        newScore = playerManagement.score;
    }
    void moveCharacter(Vector2 distance)
    {
        rb.MovePosition((Vector2)transform.position + (distance * moveSpeed * Time.deltaTime));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
