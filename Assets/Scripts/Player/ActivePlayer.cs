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

    public bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, checkRadius, _platform);
    }
}
