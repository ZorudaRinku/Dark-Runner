using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pace : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float accelerationAmount;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.right * accelerationAmount, ForceMode.Force);
        Vector3 velocity = _rigidbody.velocity;
        
        if (player.transform.position.x > transform.position.x + transform.localScale.x)
            velocity.x = Mathf.Clamp( velocity.x, 0, maxSpeed * 1.2f);
        else if (player.transform.position.x < transform.position.x - transform.localScale.x)
            velocity.x = Mathf.Clamp( velocity.x, 0, maxSpeed * 0.8f);
        
        velocity.x = Mathf.Clamp( velocity.x, 0, maxSpeed);
        _rigidbody.velocity = velocity;

        var position = transform.position;
        position.y = player.transform.position.y;
        transform.position = position;
    }
}
