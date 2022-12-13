using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class PreyBehavior : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D of the attached object.
    private Vector2 direction; // Direction of the movement.
    public float moveSpeed = 1f; // Sets the movement speed.
    public float lookRadius = 10f; // Sets the radius of how far the object is able to see.

    // Gets all needed scripts.
    public SharedBehavior sharedBehavior;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Responsible for executing the behavior of a prey GameObject.
    /// Using FixedUpdate here so that the prey stops moving when the game is paused.
    /// </summary>
    private void FixedUpdate()
    {
        // Calculates how the prey should move.
        Vector3 distance = sharedBehavior.CalculateDistance(this.transform.position);
        float angle = sharedBehavior.CalculateAngle(this.transform.position);
        float fdistance = sharedBehavior.CalculateFloatDistance(this.transform.position);
        distance.Normalize(); // Normalizes the distance vector, since we only need the direction.
        direction = -1 * distance;

        // Moves away from the player if within its look radius
        if (fdistance <= lookRadius)
        {
            rb.rotation = angle;
            sharedBehavior.MoveCharacter(direction, rb, moveSpeed);
            sharedBehavior.ObjectFlipper(rb, "flipX");
            sharedBehavior.ObjectFlipper(rb, "stayUpright");
        }
    }

    // This method visualises the look radius to help with making and testing the game
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    // This method destroys the prey if it collieds with an object that has the tag "wall".
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall") // Detect if collided with wall.
        {
            Destroy(gameObject); // Destroy the attached object
        }
    }
}
