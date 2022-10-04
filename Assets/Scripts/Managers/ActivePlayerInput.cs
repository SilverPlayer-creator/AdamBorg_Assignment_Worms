using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayerInput : MonoBehaviour
{
    public delegate void RoundStartDelegate();
    public event RoundStartDelegate OnRoundStart;
    [SerializeField] private PlayerManager _manager;
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
        StartCoroutine(RoundStart());
        TurnManager.TurnInstance.OnTurnEnding += ChangeInput;
        GetComponent<ActivePlayerWeapon>().OnThrow += ChangeInput;
        _manager.OnGameEnded += DisableInput;
    }

    void FixedUpdate()
    {
        ActivePlayer currentPlayer = _manager.GetCurrentPlayer;
        CharacterController controller = currentPlayer.Controller;
        if (_canMove)
        {
            Vector3 move = ((currentPlayer.transform.forward * _moveValue.y) + (currentPlayer.transform.right * _moveValue.x)).normalized;
            currentPlayer.transform.Rotate(new Vector3(0, _rotateValue * _rotateSpeed, 0));
            if (currentPlayer.IsGrounded() && _pressedJump)
            {
                _currentPlayerVelocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
            }
            controller.Move(move * _moveSpeed * Time.fixedDeltaTime);
        }
        if (currentPlayer.IsGrounded() && _currentPlayerVelocity.y < 0)
        {
            _currentPlayerVelocity.y = -2f;
        }

        _currentPlayerVelocity.y += _gravity * Time.fixedDeltaTime;
        _otherPlayerVelocity.y += _gravity * Time.fixedDeltaTime;
        List<ActivePlayer> allPlayers = _manager.GetAllPlayers;
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
    void DisableInput(int _int)
    {
        _canMove = false;
        _moveValue = Vector2.zero;
    }
    private void OnDisable()
    {
        TurnManager.TurnInstance.OnTurnEnding -= ChangeInput;
        GetComponent<ActivePlayerWeapon>().OnThrow -= ChangeInput;
        _manager.OnGameEnded -= DisableInput;
    }
    IEnumerator RoundStart()
    {
        yield return new WaitForSeconds(5f);
        _canMove = true;
        OnRoundStart?.Invoke();
    }
}
