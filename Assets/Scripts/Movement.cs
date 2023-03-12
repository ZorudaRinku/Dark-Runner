using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float _maxSlideTime = 1f;
    [SerializeField] private float _jumpDistanceCheck = .9f;
    [SerializeField] private float _slideReturnDistanceCheck = .9f;
    [SerializeField] private GameObject _target;
    private Rigidbody _targetRb;
    private float slideTimeCounter;
    private bool _isSliding;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetRb = _target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GroundCheck() && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) // Jump
        {
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            _isSliding = false;
        }

        if (GroundCheck() && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || // Start Sliding
                              Input.GetKeyDown(KeyCode.LeftShift)))
        {
            _isSliding = true;
        }

        if (GroundCheck() && (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || // Stop Sliding
                              Input.GetKeyUp(KeyCode.LeftShift)))
        {
            _isSliding = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flip();
        }

        
        Vector3 scale = transform.localScale;
        if (_isSliding)
        {
            scale.y = .5f;
            slideTimeCounter += Time.deltaTime;
            if (slideTimeCounter >= _maxSlideTime) // Interrupt the players slide after slideTime has been reached
            {
                _isSliding = false;
                slideTimeCounter = 0;
            }
        }
        else if(!Physics.BoxCast(transform.position, transform.localScale/2, transform.up, out hit,
                    transform.rotation, _slideReturnDistanceCheck)) // Check if the player would get stuck if they exited slide
        {
            scale.y = 1f;
        }
        transform.localScale = scale;
    }

    private void flip()
    {
        // Flip players Y position
        Vector3 newPos = transform.position;
        newPos.Set(newPos.x, -newPos.y, newPos.z);
        transform.position = newPos;
        
        // Flip player on X Axis
        Vector3 newRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(newRot.x + 180, newRot.y, newRot.z));
        
        // Flip Gravity
        Physics.gravity = -Physics.gravity;

        // Flip the players momentum
        _rigidbody.velocity = -_rigidbody.velocity;
    }

    private void FixedUpdate()
    {
        if (!_isSliding) // Don't accelerate if we're sliding
        {
            var distance = _target.transform.position.x - transform.position.x;
            var vel = _rigidbody.velocity;
            if (distance > 2f) // if distance to the box is >1, Accelerate
            {
                vel.x = Mathf.Lerp(vel.x, _targetRb.velocity.x * 1.2f, 3f * Time.deltaTime);
            } else if (distance is < 2f and > 0) // if distance to the box is <1 && >0.1, decelerate
            {
                vel.x = Mathf.Lerp(vel.x, _targetRb.velocity.x * 1.02f, 0.5f * Time.deltaTime);
            }
            else // if distance to the box is < 0.5, hard set
            {
                vel.x = Mathf.Lerp(vel.x, _targetRb.velocity.x, 1f * Time.deltaTime);
            }
            _rigidbody.velocity = vel;
        }
    }

    private bool GroundCheck()
    {
        return Physics.BoxCast(transform.position, transform.localScale/2, -transform.up, out hit,
            transform.rotation, _jumpDistanceCheck);
    }
}
