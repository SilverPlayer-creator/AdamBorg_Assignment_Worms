using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUnit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnityEngine.InputSystem.PlayerInput _input;
    //Movement Input
    [Header("Movement Input")]
    private Vector2 _moveValue;
    [SerializeField] private float _moveSpeed;
    private float _rotateValue;
    public float RotateValue
    {
        get { return _rotateValue; }
        private set { }
    }
    [SerializeField] private float _rotateSpeed;
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        private set { }
    }
    [SerializeField] private float _jumpForce;
    private bool _pressedJump;
    public bool PressedJump
    {
        get { return _pressedJump; }
        private set { }
    }

    private CharacterController _controller;
    private bool _canMove;
    [SerializeField] private float _gravity;
    private Vector3 _playerVelocity;

    [SerializeField]private float _maxMovementRange;
    private float _totalDistanceMoved;
    private Vector3 _lastPosition;

    [Header("Grounded Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask _platform;

    [Header("Weapon")]
    [SerializeField] private WeaponHolder _weaponHolder;
    public WeaponHolder WeaponHolder
    {
        get { return _weaponHolder; }
    }
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxGrenadeAmount;
    [SerializeField] private Grenades _grenadePrefab;
    private bool _canPerformActions;

    //Player stats
    [Header("Stats")]
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public static event OnTakeDamage OnDamageEvent;
    public delegate void OnTakeDamage(int currentHealth, int index);

    [Header("Active")]
    [SerializeField] private int _playerIndex;
    [SerializeField] private Camera _playerCamera;
    private bool _isActivePlayer;
    public bool IsActivePlayer
    {
        get { return _isActivePlayer; }
        private set { }
    }
    public int PlayerIndex
    {
        get { return _playerIndex; }
        private set { }
    }
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _currentHealth = _maxHealth;
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isActivePlayer)
        {
            if (_canMove)
            {
                Vector3 move = ((transform.forward * _moveValue.y) + (transform.right * _moveValue.x)).normalized;

                transform.Rotate(new Vector3(0, _rotateValue * _rotateSpeed, 0));

                if (IsGrounded() && _playerVelocity.y < 0)
                {
                    _playerVelocity.y = -2f;
                }

                if (IsGrounded() && PressedJump)
                {
                    //Debug.Log("Player should jump");
                    _playerVelocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
                }
                _controller.Move(move * _moveSpeed * Time.fixedDeltaTime);
            }
        }
        _playerVelocity.y += _gravity * Time.fixedDeltaTime;
        _controller.Move(_playerVelocity * Time.fixedDeltaTime);

        //check the max amount moved
        _totalDistanceMoved += Vector3.Distance(transform.position, _lastPosition);
        _lastPosition = transform.position;

        if (_totalDistanceMoved >= _maxMovementRange)
        {
            transform.position = _lastPosition;
            PlayerManager.GetInstance().StartCoroutine(PlayerManager.GetInstance().EndCurrentTurn());
            _canMove = false;
            _totalDistanceMoved = 0;
            //PlayerManager.GetInstance().PlayerEndedTurn();
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
    public bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, checkRadius, _platform);
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (IsActivePlayer && _canPerformActions)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _weaponHolder.FireHeldWeapon(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                _weaponHolder.FireHeldWeapon(false);
            }
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("Context performed ");
            if (IsActivePlayer && _canPerformActions) //if the player is the active one and they can do things like shoot and thre grenades
            {
                Debug.Log(this.transform.name + " Reload");
                _weaponHolder.ReloadHeldWeapon(_playerIndex);//reload the current held weapon of the current active player
            }
        }
    }
    public void ThrowGrenade(PlayerUnit player)
    {
        Grenades newGrenade = Instantiate(_grenadePrefab, _spawnPoint.position, Quaternion.identity);
        newGrenade.Initialize(player, _spawnPoint, _playerCamera);
    }
    public void AltFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsActivePlayer && _canPerformActions)
        {
            ThrowGrenade(this);
            _canPerformActions = false;
        }
    }
    public void UpdateIsActivePlayer(bool isActive)
    {
        _isActivePlayer = isActive;
        _playerCamera.enabled = isActive;
        _canMove = isActive;
        _totalDistanceMoved = 0;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Damage");
        _currentHealth -= damage;
        OnDamageEvent?.Invoke(_currentHealth, _playerIndex);
        Debug.Log("Invoke damage");
    }
    public void CanMove(bool canMove)
    {
        _canMove = canMove;
    }
    public void CanDoActions()
    {
        _canPerformActions = true;
    }
    public void ChangeInput(bool active)
    {
        if(!active)
        this._input.currentActionMap.Disable();
        else
        this._input.currentActionMap.Enable();
    }
    public void NewWeapon(Weapon newWeapon)
    {
        _weaponHolder.AddWeapon(newWeapon);
    }
}
