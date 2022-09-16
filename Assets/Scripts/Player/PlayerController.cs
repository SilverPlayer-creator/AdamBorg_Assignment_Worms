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
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask _platform;
    [SerializeField] private float _fallSpeed;
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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_rotateValue);

        //gravity
        if (_isActivePlayer)
            Debug.Log("Is grounded? " + _controller.isGrounded);
           
        
    }
    private bool IsGrounded()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - checkOffset, transform.position.z);
        //RaycastHit hit;
        //return Physics.SphereCast(transform.position, checkRadius, -transform.up, out hit, checkOffset, platform);
        return Physics.CheckSphere(transform.position, checkRadius, (int)_platform);
    }
    public void PlayerJump()
    {
        _pressedJump = true;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump function reached");
        if (context.phase == InputActionPhase.Performed)
        {
            PlayerJump();
        }
    }
    private void FixedUpdate()
    {
        if (_isActivePlayer)
        {
            Vector3 combined = ((transform.forward * _moveValue.y) + (transform.right * _moveValue.x)).normalized;

            transform.Rotate(new Vector3(0, _rotateValue * _rotateSpeed, 0));

            if (_pressedJump && _controller.isGrounded)
            {
                combined.y += _jumpForce * Time.fixedDeltaTime;
                _pressedJump = false;
            }

            combined.y += _gravity * Time.fixedDeltaTime;
            _controller.Move(combined * _moveSpeed * Time.fixedDeltaTime);
        }
    }
    Vector3 VelocityY()
    {
        Vector3 verticalVelocity = new Vector3(0, _controller.velocity.y, 0);
        if (IsGrounded() && _pressedJump)
        {
            verticalVelocity.y = _jumpForce;
            _pressedJump = false;
        }
        else if (!IsGrounded())
        {
            //Debug.Log(verticalVelocity.y);
            verticalVelocity += (Vector3.up * Physics.gravity.y) * Time.fixedDeltaTime;
        }
        return verticalVelocity;
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
