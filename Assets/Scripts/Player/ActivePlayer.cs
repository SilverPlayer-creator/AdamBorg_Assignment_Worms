using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    [Header("Grounded Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask _platform;
    private ActivePlayerHealth _playerHealth;
    public ActivePlayerHealth PlayerHealth
    {
        get { return _playerHealth; }
    }
    private CharacterController _controller;
    public CharacterController Controller
    {
        get { return _controller; }
    }
    private PlayerHeldWeapons _weaponHolder;
    public PlayerHeldWeapons WeaponHolder
    {
        get { return _weaponHolder; }
    }
    [SerializeField] private Animator _anim;
    public Animator Anim { get { return _anim; } }
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _weaponHolder = GetComponent<PlayerHeldWeapons>();
        _playerHealth = GetComponent<ActivePlayerHealth>();
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, checkRadius, _platform);
    }
}
