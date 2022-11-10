using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float spawnSpeed;
    public int spawnAmount;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generate", 0, spawnSpeed);
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    void Generate()
    {
        if(GameObject.FindGameObjectsWithTag(ObjectToSpawn.tag).Length < spawnAmount) 
        { 
            int x = Random.Range(0, Camera.main.pixelWidth);
            int y = Random.Range(0, Camera.main.pixelHeight);

            Vector3 Target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
            Target.z = 0;

            Instantiate(ObjectToSpawn, Target, Quaternion.identity); //Clones the GameObject prefab.
        }
    }
}
