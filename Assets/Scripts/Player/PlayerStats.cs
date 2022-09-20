using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private float _moveSpeed;

    public static event OnTakeDamage OnDamageEvent;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        private set { }
    }
    [SerializeField] private float _jumpForce;
    public float JumpForce
    {
        get { return _jumpForce; }
        private set { }
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        TakeDamage(50);
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnDamageEvent?.Invoke(_currentHealth);
    }
    public delegate void OnTakeDamage(int currentHealth);
}
