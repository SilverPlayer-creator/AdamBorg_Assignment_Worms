using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ActivePlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private PlayerManager _manager;
    public event Action OnEnemyDied;
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = 1;
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.fillAmount = (float)_currentHealth / (float)_maxHealth;
        if(_currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //OnEnemyDied?.Invoke();
        PlayerManager.GetInstance().RemovePlayer(this);
        PlayerManager.GetInstance().StartCoroutine(PlayerManager.GetInstance().EndCurrentTurn());
    }
    public void AddHealth(int healthGained)
    {
        _currentHealth += healthGained;
        if(_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        _healthBar.fillAmount = (float)_currentHealth / (float)_maxHealth;
    }
}
