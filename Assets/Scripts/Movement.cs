using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float _maxSlideTime = 1f;
    [SerializeField] private float _jumpDistanceCheck = .9f;
    private float slideTimeCounter;
    private bool _isSliding;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        else
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
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.right * speed, ForceMode.VelocityChange);
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = Mathf.Clamp(_rigidbody.velocity.x, 0, maxSpeed); // Possibly slow the player down when they slide
        _rigidbody.velocity = velocity;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Draw a Ray forward from GameObject toward the hit
        Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
        //Draw a cube that extends to where the hit exists
        Gizmos.DrawWireCube(transform.position + -transform.up * hit.distance, transform.localScale/2);
    }

    private bool GroundCheck()
    {
        return Physics.BoxCast(transform.position, transform.localScale/2, -transform.up, out hit,
            transform.rotation, _jumpDistanceCheck);
    }
}
