using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUnit : MonoBehaviour, IDamageable
{
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

    [Header("Grounded Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask _platform;

    [Header("Weapon")]
    [SerializeField] private Pistol _pistol;
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Grenades _grenadePrefab;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    Debug.Log("Player should jump");
                    _playerVelocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
                }

                _playerVelocity.y += _gravity * Time.fixedDeltaTime;
                _controller.Move(move * _moveSpeed * Time.fixedDeltaTime);
            }
        }
        _controller.Move(_playerVelocity * Time.fixedDeltaTime);
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
        if (context.phase == InputActionPhase.Performed)
        {
            _weaponHolder.GetComponent<WeaponHolder>().FireHeldWeapon(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _weaponHolder.GetComponent<WeaponHolder>().FireHeldWeapon(false);
        }
    }
    public void ThrowGrenade(PlayerUnit player)
    {
        Grenades newGrenade = Instantiate(_grenadePrefab, _spawnPoint.position, Quaternion.identity);
        newGrenade.Initialize(player);
    }
    public void AltFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsActivePlayer)
        {
            ThrowGrenade(this);
        }
    }
    public void UpdateIsActivePlayer(bool isActive)
    {
        _isActivePlayer = isActive;
        _playerCamera.enabled = isActive;
        _canMove = isActive;
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
}
