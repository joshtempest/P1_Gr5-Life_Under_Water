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

    //This method is responsible for flipping the object in various ways when used. Takes Rigidbody2D and a string as parameters.
    public void ObjectFlipper(Rigidbody2D rb, string task)
    {
        
        if(task == "stayUpright") // This portion is responsible for making the object in question always stand upright.
        {
            if (rb.transform.eulerAngles.z >= 90 && rb.transform.eulerAngles.z <= 270) // Flips the sprite if upside down.
            {
                float x = rb.transform.localScale.x; // Sets the "x" variable to whatever the localScale.x value currently is.
                float y = -1 * (System.Math.Abs(rb.transform.localScale.y)); // Takes the current localScale.y and makes sure that it is always a negative value.
                rb.transform.localScale = new Vector2(x, y); // Sets the localScale of the object to the values of the "x" and "y" variables.
            }
            else 
            {
                float x = rb.transform.localScale.x; // Sets the "x" variable to whatever the localScale.x value currently is.
                float y = System.Math.Abs(rb.transform.localScale.y); // Sets the "y" variable to whatever the localScale.y value currently is, as well as ensuring that the scale is a positive value.
                rb.transform.localScale = new Vector2(x, y); // Sets the localScale of the object to the values of the "x" and "y" variables.
            }
        }
        if(task == "flipX") // This portion is responsible for flipping the object on the "X" axis.
        {
            float y = rb.transform.localScale.y; // Sets the "y" variable to whatever the localScale.y value currently is.
            float x = -1 * (System.Math.Abs(rb.transform.localScale.x)); // Takes the current localScale.x and makes sure that it is always a negative value.
            rb.transform.localScale = new Vector2(x, y); // Sets the localScale of the object to the values of the "x" and "y" variables.
        }
        if (task == "unflipX") // This portion is responsible for undoing the flip on the "X" axis.
        {
            float y = rb.transform.localScale.y; // Sets the "y" variable to whatever the localScale.y value currently is.
            float x = System.Math.Abs(rb.transform.localScale.x); // Takes the current localScale.x and makes sure that it is always a positive value.
            rb.transform.localScale = new Vector2(x, y); // Changes the localScale of the object to the value of the "x" and "y" variables.
        }
    }

    // This method is responsible for moving the object. Takes a Vector2, a RigidBody2D and a float as parameters.
    public void MoveCharacter(Vector2 direction, Rigidbody2D rb, float moveSpeed)
    {
        rb.MovePosition((Vector2)rb.transform.position + (direction * moveSpeed * Time.deltaTime)); // Moves the object based on the product of the direction vector, moveSpeed and time.
    }
}
