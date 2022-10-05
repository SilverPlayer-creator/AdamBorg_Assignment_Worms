using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayerInput : MonoBehaviour
{
    public delegate void RoundStartDelegate();
    public event RoundStartDelegate OnRoundStart;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private TurnManager _turnManager;
    [Header("Movement Input")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    private Vector2 _moveValue;
    private float _rotateValue;
    private bool _canMove;
    private bool _pressedJump;
    private Vector3 _currentPlayerVelocity;
    private Vector3 _otherPlayerVelocity;
    void Start()
    {
        _turnManager.OnTurnEnding += ChangeInput;
        GetComponent<ActivePlayerWeapon>().OnThrow += ChangeInput;
        _playerManager.OnGameEnded += () => ChangeInput(false);
        _turnManager.OnCountDown += RoundStart;
        AudioManager.AudioInstance().PlaySound("RobotStart");
    }

    void FixedUpdate()
    {
        ActivePlayer currentPlayer = _playerManager.GetCurrentPlayer;
        CharacterController controller = currentPlayer.Controller;
        if (_canMove)
        {
            Vector3 move = ((currentPlayer.transform.forward * _moveValue.y) + (currentPlayer.transform.right * _moveValue.x)).normalized;
            currentPlayer.transform.Rotate(new Vector3(0, _rotateValue * _rotateSpeed, 0));
            if (currentPlayer.IsGrounded() && _pressedJump)
            {
                _currentPlayerVelocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
                AudioManager.AudioInstance().PlaySound("Jump");
            }
            controller.Move(move * _moveSpeed * Time.fixedDeltaTime);
        }
        if (currentPlayer.IsGrounded() && _currentPlayerVelocity.y < 0)
        {
            _currentPlayerVelocity.y = -2f;
        }

        _currentPlayerVelocity.y += _gravity * Time.fixedDeltaTime;
        _otherPlayerVelocity.y += _gravity * Time.fixedDeltaTime;
        List<ActivePlayer> allPlayers = _playerManager.GetAllPlayers;
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] != currentPlayer)
            {
                allPlayers[i].Controller.Move(_otherPlayerVelocity * Time.fixedDeltaTime);
            }
            allPlayers[i].Controller.Move(_currentPlayerVelocity * Time.fixedDeltaTime);
        }
        controller.Move(_currentPlayerVelocity * Time.fixedDeltaTime);
        if(_moveValue.x != 0 && currentPlayer.IsGrounded() || _moveValue.y != 0 && currentPlayer.IsGrounded())
        {
            currentPlayer.Anim.SetBool("Walk_Anim", true);
        }
        else
        {
            currentPlayer.Anim.SetBool("Walk_Anim", false);
        }
    }
    public void MovePlayer(InputAction.CallbackContext context)
    {
        if(_canMove)
        _moveValue = context.ReadValue<Vector2>();
    }
    public void RotatePlayer(InputAction.CallbackContext context)
    {
        _rotateValue = context.ReadValue<float>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _pressedJump = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _pressedJump = false;
        }
    }
    private void ChangeInput(bool active)
    {
        _canMove = active;
        if (!active)
            _moveValue = Vector2.zero;
    }
    private void RoundStart(int countDown)
    {
        if(countDown <= 0)
        {
            _canMove = true;
            OnRoundStart?.Invoke();
        }
    }
    private void OnDisable()
    {
        _turnManager.OnTurnEnding -= ChangeInput;
        GetComponent<ActivePlayerWeapon>().OnThrow -= ChangeInput;
        _playerManager.OnGameEnded -= () => ChangeInput(false);
    }
}
