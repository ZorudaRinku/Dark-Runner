using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if object is above or below the x-axis
        if(gameObject.transform.position.y >= 0)
        {

            // non-kill objects set to white on top side
            gameObject.GetComponent<Renderer>().material.color = Color.white;

            // check if object has the kill tag
            if(gameObject.tag == "kill")
            {
                gameObject.GetComponent<Renderer>().material.color = Color.black;
            }

        }
        else
        {

            // non-kill objects set to black on bottom side
            gameObject.GetComponent<Renderer>().material.color = Color.black;

            // check if object has the kill tag
            if (gameObject.tag == "kill")
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }

        }
    }
}
