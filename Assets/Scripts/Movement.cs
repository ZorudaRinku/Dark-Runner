using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool slowed;
    private bool flipped;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetRb = _target.GetComponent<Rigidbody>();
        flipped = false;
    }

    
    // Update is called once per frame, manages controls
    void Update()
    {
        // jump using W or up arrow
        if (GroundCheck() && ((!flipped && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) || (flipped && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))))) // Jump
        {
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            _isSliding = false;
        }

        // start sliding 
        if (GroundCheck() && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            _isSliding = true;
        }

        // stop sliding 
        if (GroundCheck() && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            _isSliding = false;
        }

        // flip 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flip();
        }   

        // Reload the scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }

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
        
        // Flip Player Momentum
        var velocity = _rigidbody.velocity;
        velocity.y = -velocity.y;
        _rigidbody.velocity = velocity;

        // Slow down time and begin lerping back to 1x time
        Time.timeScale = 0.8f;
        slowed = true;

        // Flip value of flipped Boolean
        flipped = !flipped;
    }

    private void FixedUpdate()
    {
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
        else if (!Physics.BoxCast(transform.position, transform.localScale / 2, transform.up, out hit,
                    transform.rotation, _slideReturnDistanceCheck)) // Check if the player would get stuck if they exited slide
        {
            scale.y = 1f;
        }
        transform.localScale = scale;

        if (slowed)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 1f * Time.deltaTime);
            if (Time.timeScale > 0.98)
            {
                slowed = false;
                Time.timeScale = 1;
            }
        }

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

    // reloads the current scene
    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
