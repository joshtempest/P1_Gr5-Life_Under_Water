using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectSpawning : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float spawnSpeed;
    public int spawnAmount;
    public float minDistanceFromPlayer;

    GameObject SpawnerBox;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Generate", 0, spawnSpeed);
        //InvokeRepeating("SpawnObject", 0, spawnSpeed);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Generate()
    {
        //Vector3 spawnPosition = new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0f);

        if (GameObject.FindGameObjectsWithTag(ObjectToSpawn.tag).Length < spawnAmount)
        {
            SpawnerBox = GameObject.Find("SpawnerBox"); //Finds the SpawnerBox object and attaches it to the SpawnerBox GameObject variable.

            float spawnerWidth = SpawnerBox.GetComponent<SpriteRenderer>().bounds.size.x;
            float spawnerHeight = SpawnerBox.GetComponent<SpriteRenderer>().bounds.size.y;

            float x = Random.Range(-spawnerWidth, spawnerWidth);
            float y = Random.Range(-spawnerHeight, spawnerHeight);

            Vector3 spawnPosition = new Vector3(x, y, 0);

            spawnPosition.z = 0;


            Vector3 playerPosition = GameObject.Find("Player").transform.position; //Gets the position of the player object.

            Vector3 distanceFromPlayer = playerPosition - spawnPosition; //Calculates the differentiating vector between playerPosition and spawnPosition.

            float fdistance = Mathf.Sqrt(Mathf.Pow(distanceFromPlayer.x, 2) + Mathf.Pow(distanceFromPlayer.y, 2)); //Finds pythagoras to find the distance to the player as a float

            //print(fdistance);

            if (fdistance > minDistanceFromPlayer) //Spawns object if the distance to the player is great enough.
            {
                Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity); //Clones the GameObject prefab.
            }

           
       }
   }
}
