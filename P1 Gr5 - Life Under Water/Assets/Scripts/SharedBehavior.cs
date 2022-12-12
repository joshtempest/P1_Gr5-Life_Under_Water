using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SharedBehavior : MonoBehaviour
{
    bool flipped;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This method ensures that the sprite of an object is always facing upright. (This function has been replaced by 'ObjectFlipper')
    public void SpriteFlipper(Rigidbody2D rb, SpriteRenderer spriteRenderer)
    {
        if (rb.transform.eulerAngles.z >= 90 && rb.transform.eulerAngles.z <= 270) //Flips the sprite if upside down.
        {
            spriteRenderer.flipY = true; // Flips the sprite on the Y axis
        }
        else
        {
            spriteRenderer.flipY = false; //Unflips if the "if" statement does not match.
        }
    }

    // This method is responsible for flipping the object in various ways when used. 
    // Takes Rigidbody2D and a string as parameters.
    // The string is used to determine which kind of task to perform.
    public void ObjectFlipper(Rigidbody2D rb, string task)
    {
        // This portion is responsible for making the object in question always stand upright.
        if (task == "stayUpright")
        {
            // Flips the sprite if upside down.
            if (rb.transform.eulerAngles.z >= 90 && rb.transform.eulerAngles.z <= 270)
            {
                float x = rb.transform.localScale.x;
                float y = -1 * (System.Math.Abs(rb.transform.localScale.y));
                rb.transform.localScale = new Vector2(x, y);
            }
            // Unflips the sprite if not upside down.
            else
            {
                float x = rb.transform.localScale.x;
                float y = System.Math.Abs(rb.transform.localScale.y);
                rb.transform.localScale = new Vector2(x, y);
            }
        }
        // This portion is responsible for flipping the object on the "X" axis.
        if (task == "flipX")
        {
            float y = rb.transform.localScale.y;
            float x = -1 * (System.Math.Abs(rb.transform.localScale.x));
            rb.transform.localScale = new Vector2(x, y);
        }
        // This portion is responsible for undoing the flip on the "X" axis.
        if (task == "unflipX")
        {
            float y = rb.transform.localScale.y;
            float x = System.Math.Abs(rb.transform.localScale.x);
            rb.transform.localScale = new Vector2(x, y);
        }
    }

    // This method is responsible for moving the object.
    // Takes a Vector2, RigidBody2D and float as parameters.
    // The Vector2 parameter determines the direction of the movement
    // The float parameter determines the movement speed.
    public void MoveCharacter(Vector2 direction, Rigidbody2D rb, float moveSpeed)
    {
        // Moves the object based on the product of the direction vector, moveSpeed and time.
        rb.MovePosition((Vector2)rb.transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
