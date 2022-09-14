using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    private bool _canMove;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform inputSpace;
    private Rigidbody _body;

    private Vector3 _playerStartPosition;
    [SerializeField] private float maxDistance;

    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask platform;
    private bool _jumpButtonPressed;
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _canMove = true;
        _playerStartPosition= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float playerMovedDistance = Vector3.Distance(_playerStartPosition, transform.position);
        if(transform.position.x >= _playerStartPosition.x + maxDistance)
        {
            transform.position = new Vector3(_playerStartPosition.x + maxDistance, transform.position.y, transform.position.z);
            Debug.Log("Player can not move anymore on the right");
        }
        if (transform.position.x <= _playerStartPosition.x - maxDistance)
        {
            transform.position = new Vector3(_playerStartPosition.x - maxDistance, transform.position.y, transform.position.z);
            Debug.Log("Player can not move anymore on the left");
        }
        if(transform.position.z >= _playerStartPosition.z + maxDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _playerStartPosition.z + maxDistance);
            Debug.Log("Player can not move forward anymore");
        }
        if (transform.position.z <= _playerStartPosition.z - maxDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _playerStartPosition.z - maxDistance);
            Debug.Log("Player can not move backwards anymore");
        }
        //Debug.Log("Player has moved " + playerMovedDistance);
        Rotate();
        Debug.Log(TouchingFloor());
        if (Input.GetKeyDown(KeyCode.Space) && TouchingFloor())
        {
            _jumpButtonPressed = true;
            
        }
    }
    private void FixedUpdate()
    {
        if(_canMove)
        Move();

        if (_jumpButtonPressed)
        {
            Jump();
            _jumpButtonPressed = false;
        }
    }
    private Vector3 GetRight()
    {
        Vector3 _input = new Vector3(inputSpace.right.x, 0, inputSpace.right.z).normalized;
        return _input;
    }
    private Vector3 GetForward()
    {
        Vector3 _input = new Vector3(inputSpace.forward.x, 0, inputSpace.forward.z).normalized;
        return _input;
    }
    private void Move()
    {
        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 direction = GetForward() * Input.GetAxis("Vertical") + GetRight() * Input.GetAxis("Horizontal");
        //_body.velocity = new Vector3(moveVector.x * speed, _body.velocity.y, moveVector.y * speed);
        Vector3 velocity = direction * speed;
        _body.velocity = new Vector3(velocity.x, _body.velocity.y, velocity.z);

        
    }
    void Jump()
    {
        //_body.AddForce(Vector3.up * jumpForce);
        Vector3 jumpDir = new Vector3(_body.velocity.x, jumpForce, _body.velocity.z);
        _body.AddForce(Vector3.up * jumpForce);
    }
    private void Rotate()
    {
        var forward = GetForward();
        float angle = Mathf.Acos(forward.z) * Mathf.Rad2Deg;
        angle = forward.x < 0f ? 360f - angle : angle;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - checkOffset, transform.position.z);
        Gizmos.DrawWireSphere(pos, checkRadius);
    }
    private bool TouchingFloor()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        RaycastHit hit;
        return Physics.SphereCast(transform.position, checkRadius, -transform.up, out hit, checkOffset, platform);
    }
}
