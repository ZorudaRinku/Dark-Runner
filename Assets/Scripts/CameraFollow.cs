using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private float xOffset;
    private Camera camera;
    private Vector3 position;
    private Vector3 viewPosition;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // updates position
        position = transform.position;
        
        // checks which side player is on and calculates movement based on that
        if (target.transform.position.y > 0) // Player is on top
        {
            position.y = Mathf.Lerp(position.y, target.transform.position.y/2, 4f * Time.deltaTime);
        } else // Player is on bottom
        {
            position.y = Mathf.Lerp(position.y, target.transform.position.y/2, 4f * Time.deltaTime);
        }
        
        // Move position.z back based on the absolute of the playersCharacter y position
        position.z = Mathf.Lerp(position.z, -Mathf.Abs(playerCharacter.transform.position.y), 4f * Time.deltaTime) - 2f;

        // Relates camera view to player object
        viewPosition = camera.WorldToViewportPoint(playerCharacter.transform.position);

        // Checks if player is in camera view bounds
        if (viewPosition.x >= 0 && viewPosition.x <= 1 && viewPosition.y >= 0 && viewPosition.y <= 1 && viewPosition.z > 0)
        {
            Debug.Log("Player Character is in view!");
        }
        else
        {
            Debug.Log("Not in view!");
        }

        position.x = target.transform.position.x + xOffset; 
        transform.position = position;


    }
}
