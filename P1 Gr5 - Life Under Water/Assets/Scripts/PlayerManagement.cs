using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    //Based on "How to make an object follow the mouse in Unity" by Karolio: https://www.youtube.com/watch?v=mF_BB_YsyDk
    Camera cam;
    public Vector3 sizeUp;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object")
        {
            transform.localScale += sizeUp;
            Destroy(other.gameObject);
        }
    }
}
