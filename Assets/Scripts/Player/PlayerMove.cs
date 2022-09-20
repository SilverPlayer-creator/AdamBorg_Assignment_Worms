using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput), typeof(GroundCheck))]
public class PlayerMove : MonoBehaviour
{
    private CharacterController _controller;
    private bool _canMove;
    private PlayerInput _input;
    private GroundCheck _groundCheck;
    private PlayerStats _stats;
    private Vector3 _playerVelocity;
    [SerializeField] private float _gravity;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<PlayerInput>();
        _groundCheck = GetComponent<GroundCheck>();
        _stats = GetComponent<PlayerStats>();
    }
    private void FixedUpdate()
    {
        if (GetComponent<PlayerActive>().IsActivePlayer)
        {
            if (_canMove)
            {
                Vector3 move = ((transform.forward * _input.MoveValue.y) + (transform.right * _input.MoveValue.x)).normalized;

                transform.Rotate(new Vector3(0, _input.RotateValue * _input.RotateSpeed, 0));

                if (_groundCheck.IsGrounded() && _playerVelocity.y < 0)
                {
                    _playerVelocity.y = -2f;
                }

                if (_groundCheck.IsGrounded() && _input.PressedJump)
                {
                    Debug.Log("Player should jump");
                    _playerVelocity.y = Mathf.Sqrt(_stats.JumpForce * -2 * _gravity);
                }

                _playerVelocity.y += _gravity * Time.fixedDeltaTime;
                _controller.Move(move * _stats.MoveSpeed * Time.fixedDeltaTime);
            }
        }
        _controller.Move(_playerVelocity * Time.fixedDeltaTime);
    }
    public void CanMove(bool canMove)
    {
        _canMove = canMove;
    }
}
