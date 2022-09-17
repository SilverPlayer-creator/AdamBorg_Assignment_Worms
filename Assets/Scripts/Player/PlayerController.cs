using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private CharacterController _controller;
    [SerializeField] private InputAction rotation;
    private Vector2 _moveValue;
    [SerializeField][Range(-10f, 0f)] private float _gravity;
    private float _rotateValue;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private bool _pressedJump;
    private Vector3 _playerVelocity;

    [Header("Grounded Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask _platform;
    private bool _isGrounded;

    [Header("Active")]
    [SerializeField] private int _playerIndex;
    [SerializeField] private Camera _playerCamera;
    public int PlayerIndex
    {
        get { return _playerIndex; }
        private set { }
    }
    private bool _isActivePlayer;
    public bool IsActivePlayer
    {
        get { return _isActivePlayer; }
        private set { }
    }
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PickupManager.GetInstance();
        _controller.Move(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_rotateValue);

        //gravity
        if (_isActivePlayer)
            Debug.Log("Is grounded? " + _controller.isGrounded);
           
        
    }
    public void PlayerJump(bool pressed)
    {
        _pressedJump = pressed;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump function reached");
        if (context.phase == InputActionPhase.Performed)
        {
            PlayerJump(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            PlayerJump(false);
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, checkRadius, _platform);
    }
    private void FixedUpdate()
    {
        if (_isActivePlayer)
        {
            Vector3 move = ((transform.forward * _moveValue.y) + (transform.right * _moveValue.x)).normalized;

            transform.Rotate(new Vector3(0, _rotateValue * _rotateSpeed, 0));

            if(IsGrounded() && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
                Debug.Log("Apply gravity");
            }

            if(IsGrounded() && _pressedJump)
            {
                Debug.Log("Player should jump");
                _playerVelocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
            }
            else if(IsGrounded() && !_pressedJump)
            {
                Debug.LogError("Player is grounded, jump input not registered");
            }
            else if(!IsGrounded() && _pressedJump)
            {
                Debug.LogError("Jump input registered, player is not grounded");
            }
    
            _playerVelocity.y += _gravity * Time.fixedDeltaTime;
            _controller.Move(_playerVelocity * Time.fixedDeltaTime);
            _controller.Move(move * _moveSpeed * Time.fixedDeltaTime);
        }
    }
    public void MovePlayer(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }
    public void RotatePlayer(InputAction.CallbackContext context)
    {

        _rotateValue = context.ReadValue<float>();
    }
    private void OnEnable()
    {
        rotation.Enable();
    }
    private void OnDisable()
    {
        rotation.Disable();
    }
    public void UpdateIsActivePlayer(bool isActive)
    {
        _isActivePlayer = isActive;
        _playerCamera.enabled = isActive;
    }
    private void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - checkOffset, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, checkRadius);
    }
}
