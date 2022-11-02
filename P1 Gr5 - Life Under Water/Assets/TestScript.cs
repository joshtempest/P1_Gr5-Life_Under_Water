using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    char c;
    double d;
    float f;
    int i;
    string s;
    
    // Start is called before the first frame update
    void Start()
    {
        c = 'c';
        d = 0.7;
        f = 1.1f;
        i = 1;
        s = "This is a string.";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            c = 'a';
            d = 0.0;
            f = 1.2f;
            i = 0;
            s = "Strings";
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("char: " + c + ", double: " + d + ", float: " + f + ", int: " + i + " & string: " + s + ".");
        }
    }
}
