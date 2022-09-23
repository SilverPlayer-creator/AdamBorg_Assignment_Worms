using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    [SerializeField] private PlayerManager _manager;
    [Header("Grounded Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkOffset;
    [SerializeField] private LayerMask _platform;
    [SerializeField] private float _gravity;
    private Vector3 _playerVelocity;
    private void FixedUpdate()
    {
        //_manager.GetComponent<ActivePlayerInput>().Velocity(_gravity);
        //_playerVelocity.y += _gravity * Time.fixedDeltaTime;
        //GetComponent<CharacterController>().Move(_playerVelocity * Time.fixedDeltaTime);
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, checkRadius, _platform);
    }
}
