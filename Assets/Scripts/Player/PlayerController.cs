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
    private float _rotateValue;
    private float _rotateRate = 4;
    private float _nextRotateTime = 0;
    [SerializeField] private float _speed;

    [Header("Grounded Check")]
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask platform;

    [Header("Active")]
    [SerializeField] private int _playerIndex;
    private bool _isActivePlayer;
    private bool _canMove;
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
        Debug.Log(_rotateValue);
    }
    private void FixedUpdate()
    {
        if (PlayerManager.GetInstance().isItPlayersTurn(_playerIndex))
        {
            var moveVector = new Vector3(_moveValue.x, 0, _moveValue.y);

            Vector3 forward = transform.forward * _moveValue.y;

            Vector3 combined = ((transform.forward * _moveValue.y) + (transform.right * _moveValue.x)).normalized;
            //var newMoveVector = new Vector3(forward * _moveValue.y, 0, right * _moveValue.x);
            //_controller.Move(moveVector * _speed);
            _controller.Move(combined * _speed);

            if(Time.time >= _nextRotateTime)
            {
                transform.Rotate(new Vector3(0, _rotateValue, 0), 90);
                _nextRotateTime = Time.time + 1f / _rotateRate;
            }
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
}
