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

    //This method ensures that the object is always facing upright.
    public void SpriteFlipper(Rigidbody2D rb, SpriteRenderer spriteRenderer)
    {
        if (rb.transform.eulerAngles.z >= 90 && rb.transform.eulerAngles.z <= 270) //Flips the sprite if upside down.
        {
            spriteRenderer.flipY = true;
            //print("Flipped!");
        }

        else
        {
            spriteRenderer.flipY = false; //Unflips if the "if" statement does not match.
            //print("Unflipped!");
        }
    }
    //This method is responsible for moving the object.
    //public void moveCharacter(Vector2 distance, Rigidbody2D rb, float moveSpeed)
    //{
    //    rb.MovePosition((Vector2)transform.position + (distance * moveSpeed * Time.deltaTime));
    //}
    public void ObjectFlipper(Rigidbody2D rb, string task)
    {
        
        if(task == "stayUpright")
        {
            if (rb.transform.eulerAngles.z >= 90 && rb.transform.eulerAngles.z <= 270) //Flips the sprite if upside down.
            {
                float x = rb.transform.localScale.x;
                float y = -1 * (System.Math.Abs(rb.transform.localScale.y)); //Ensures that the scale is always a positive value
                rb.transform.localScale = new Vector2(x, y);

                //spriteRenderer.flipY = true;
                //print("Flipped!");
            }
            else
            {
                float x = rb.transform.localScale.x;
                float y = System.Math.Abs(rb.transform.localScale.y); //Ensures that the scale is a positive value
                rb.transform.localScale = new Vector2(x, y);

                //spriteRenderer.flipY = false; //Unflips if the "if" statement does not match.
                //print("Unflipped!");
            }
        }
        if(task == "flipX")
        {
            float y = rb.transform.localScale.y;
            float x = -1 * (System.Math.Abs(rb.transform.localScale.x)); //Ensures that the scale is always a negative value
            rb.transform.localScale = new Vector2(x, y);
        }
        if (task == "unflipX")
        {
            float y = rb.transform.localScale.y;
            float x = System.Math.Abs(rb.transform.localScale.x); //Ensures that the scale is always a positive value
            rb.transform.localScale = new Vector2(x, y);
        }
    }

    public void MoveCharacter(Vector2 direction, Rigidbody2D rb, float moveSpeed)
    {
        rb.MovePosition((Vector2)rb.transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
