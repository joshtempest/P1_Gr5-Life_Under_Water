using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This method ensures that the object is always facing upright.
    public void SpriteFlipper(Rigidbody2D rb, SpriteRenderer spriteRenderer)
    {
        if (rb.transform.eulerAngles.z >= 90 && rb.transform.eulerAngles.z <= 270) //Flips the sprite if upside down.
        {
            spriteRenderer.flipY = true;
            print("Flipped!");
        }

        else
        {
            spriteRenderer.flipY = false; //Unflips if the "if" statement does not match.
            print("Unflipped!");
        }
    }
    //This method is responsible for moving the object.
    //public void moveCharacter(Vector2 distance, Rigidbody2D rb, float moveSpeed)
    //{
    //    rb.MovePosition((Vector2)transform.position + (distance * moveSpeed * Time.deltaTime));
    //}
    public void MoveCharacter(Vector2 direction, Rigidbody2D rb, float moveSpeed)
    {
        rb.MovePosition((Vector2)rb.transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
