using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    [Header("Grounded Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask _platform;
    [SerializeField] private Animator _anim;
    [SerializeField] private ActivePlayerHealth _playerHealth;
    public Animator Anim { get { return _anim; } }
    public ActivePlayerHealth PlayerHealth { get { return _playerHealth; } }
    public CharacterController Controller { get { return _controller; } }
    public PlayerHeldWeapons WeaponHolder { get { return _weaponHolder; } }
    private CharacterController _controller;
    private PlayerHeldWeapons _weaponHolder;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _weaponHolder = GetComponent<PlayerHeldWeapons>();
    }
    private void Update()
    {
        _anim.SetBool("Grounded", IsGrounded());
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, checkRadius, _platform);
    }
}
