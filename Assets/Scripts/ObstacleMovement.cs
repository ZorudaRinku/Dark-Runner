using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleMovement : MonoBehaviour
{
    private Vector3 _center;
    [SerializeField] private Vector3 targetOffset;
    private Vector3 currentTarget;
    [SerializeField] private float timeToReachTarget = 0.3F;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _center = transform.position;
        currentTarget = _center + targetOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // SmoothDamp to move the object to the target then back to the center
        transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref velocity, timeToReachTarget);
        if (Vector3.Distance(transform.position, currentTarget) < 0.3f)
        {
            currentTarget = currentTarget == _center ? _center + targetOffset : _center;
        }
    }
}
