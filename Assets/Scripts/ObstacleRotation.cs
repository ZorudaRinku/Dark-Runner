using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    [SerializeField] private Vector3 targetAngleOffset;
    [SerializeField] private float timeToReachTarget = 0.3F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Add the rotation to the object to reach the target by the timeToReachTarget
        transform.Rotate(targetAngleOffset * Time.deltaTime / timeToReachTarget);
    }
}
