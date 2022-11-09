using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public Transform player;
    
    public int newScore;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneControls.isPaused == false)
        {
            Vector3 direction = player.position - transform.position; //Finds the distance between enemy and player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Gets the angle between the enemy and the player in radians which is then converted
            rb.rotation = angle; //rotates to face the player
            direction.Normalize();
            movement = direction;
        }
        
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
        GameObject Player = GameObject.Find("Player");
        PlayerManagement playerManagement = Player.GetComponent<PlayerManagement>();
        newScore = playerManagement.score;
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
