using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// This script controls everything to do with spawning of objects.
/// </summary>
public class ObjectSpawning : MonoBehaviour
{
    public GameObject ObjectToSpawn; // The object that should spawn.
    public float spawnSpeed; // The speed that the objects spawns.
    public int spawnAmount; // The amount of objects to spawn.
    public float minDistanceFromPlayer; // The minimum distance from the player allowed for spawning.
    GameObject SpawnerBox;

    // Gets all needed scripts.
    GameObject BehaviorController;
    SharedBehavior sharedBehavior;

    void Start()
    {
        BehaviorController = GameObject.Find("BehaviorController"); // Finds the BehaviorController object and attaches it to the SpawnerBox GameObject variable.
        sharedBehavior = BehaviorController.GetComponent<SharedBehavior>(); // Gets the SharedBehavior script from the BehaviorController.
        SpawnerBox = GameObject.Find("SpawnerBox"); // Finds the SpawnerBox object and attaches it to the SpawnerBox GameObject variable.
        InvokeRepeating("Generate", 0, spawnSpeed);
    }

    // This function is responsible for generating objects.
    void Generate()
    {
        if (GameObject.FindGameObjectsWithTag(ObjectToSpawn.tag).Length < spawnAmount) // Doesn't allow for spawning more that the specified spawn amount.
        {
            float spawnerWidth = SpawnerBox.GetComponent<SpriteRenderer>().bounds.size.x;
            float spawnerHeight = SpawnerBox.GetComponent<SpriteRenderer>().bounds.size.y;
            float x = Random.Range(-spawnerWidth, spawnerWidth);
            float y = Random.Range(-spawnerHeight, spawnerHeight);

            Vector3 spawnPosition = new Vector3(x, y, 0);

            float fdistance = sharedBehavior.CalculateFloatDistance(spawnPosition); // Calculates the distance to the player as a float.

            if (fdistance > minDistanceFromPlayer) // Spawns object if the distance to the player is great enough.
            {
                Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity); // Clones the GameObject prefab.
            }
        }
    }
}
