using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script contains shared behaviors that are used in PreyBehavior and EnemyBehavior.
/// </summary>
public class SharedBehavior : MonoBehaviour
{
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

    // This method calculates the differentiating vector between the player position and the other given object.
    // Takes the position of the object as a parameter (Vector3).
    public Vector3 CalculateDistance(Vector3 objectPosition)
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Vector3 distance = playerPosition - objectPosition;
        return distance;
    }

    // This method calculates the angle to the player and returns it as a float.
    // Takes the Transform of an object as a parameter.
    public float CalculateAngle(Vector3 objectPosition)
    {
        Vector3 distance = CalculateDistance(objectPosition);
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        return angle;
    }

    // This method calculates the distance between an object and the player and returns it as a float.
    // Takes the Transform of the object as a parameter.
    public float CalculateFloatDistance(Vector3 objectPosition)
    {
        Vector3 distance = CalculateDistance(objectPosition);
        float fdistance = Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2));
        return fdistance;
    }

    // This method is responsible for making an object move.
    // Takes Vector2, RigidBody2D and float as parameters.
    public void MoveCharacter(Vector2 direction, Rigidbody2D rb, float moveSpeed)
    {
        // Moves the object based on the product of the direction vector, moveSpeed and time.
        rb.MovePosition((Vector2)rb.transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
