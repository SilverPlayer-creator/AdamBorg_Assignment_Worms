using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerManager _manager;
    [Header("Movement Input")]
    private Vector2 _moveValue;
    [SerializeField] private float _moveSpeed;
    private float _rotateValue;
    [SerializeField] private float _rotateSpeed;
    private CharacterController _controller;
    private bool _canMove;
    private bool _pressedJump;
    [SerializeField] private float _jumpForce;

    private Vector3 _playerVelocity;
    private Vector3 _otherVelocity;
    public Vector3 PlayerVelocity
    {
        get { return _playerVelocity; }
        set { _playerVelocity = value; }
    }
    [SerializeField] private float _gravity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ActivePlayer currentPlayer = _manager.GetCurrentPlayer();
        _controller = currentPlayer.GetComponent<CharacterController>();
        Vector3 move = ((currentPlayer.transform.forward * _moveValue.y) + (currentPlayer.transform.right * _moveValue.x)).normalized;
        currentPlayer.transform.Rotate(new Vector3(0, _rotateValue * _rotateSpeed, 0));
        if (currentPlayer.IsGrounded() && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        if (currentPlayer.IsGrounded() && _pressedJump)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
        }

        _playerVelocity.y += _gravity * Time.fixedDeltaTime;
        _otherVelocity.y += _gravity * Time.fixedDeltaTime;
        List<ActivePlayer> allPlayers = _manager.GetAllPlayers();
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] != currentPlayer)
            {
                allPlayers[i].GetComponent<CharacterController>().Move(_otherVelocity * Time.fixedDeltaTime);
            }
            allPlayers[i].GetComponent<CharacterController>().Move(_playerVelocity * Time.fixedDeltaTime);
        }
        _controller.Move(_playerVelocity * Time.fixedDeltaTime);
        _controller.Move(move * _moveSpeed * Time.fixedDeltaTime);
    }
    public void MovePlayer(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }
    public void RotatePlayer(InputAction.CallbackContext context)
    {
        _rotateValue = context.ReadValue<float>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump function reached");
        if (context.phase == InputActionPhase.Performed)
        {
            _pressedJump = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _pressedJump = false;
        }
    }
    public void Velocity(float gravity)
    {
        _playerVelocity.y += gravity * Time.fixedDeltaTime;
    }
}
