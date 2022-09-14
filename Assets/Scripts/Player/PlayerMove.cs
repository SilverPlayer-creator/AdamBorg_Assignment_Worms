using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivePlayer { Player1, Player2}
public class PlayerMove : MonoBehaviour
{
    private float _x, _z;
    private Rigidbody _body;
    [SerializeField] private float _speed;

    [SerializeField] private ActivePlayer _activePlayer;

    private bool _isActivePlayer = true;
    private bool _canMove = true;


    [Header("Ground Check")]
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask platform;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _x = Input.GetAxis("Horizontal");
        _z = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(_x, 0, _z);
        if(_canMove && _isActivePlayer)
        _body.MovePosition(transform.position += move * _speed);
    }
    private bool TouchingFloor()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        RaycastHit hit;
        return Physics.SphereCast(transform.position, checkRadius, -transform.up, out hit, checkOffset, platform);
    }
}
