using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float xOffset;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (target.transform.position.y > 0) // Player is on top
        {
            position.y = Mathf.Lerp(position.y, target.transform.position.y/2, 4f * Time.deltaTime);
        } else if (target.transform.position.y < 0) // Player is on bottom
        {
            position.y = Mathf.Lerp(position.y, target.transform.position.y/2, 4f * Time.deltaTime);
        }

        position.x = target.transform.position.x + xOffset; 

        transform.position = position;
    }
}
