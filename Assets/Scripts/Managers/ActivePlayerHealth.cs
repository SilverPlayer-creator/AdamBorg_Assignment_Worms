using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ActivePlayerHealth : MonoBehaviour, IDamageable
{
    public ActivePlayer ActivePlayer
    {
        get { return _activePlayer; }
    }
    public delegate void PlayerDeath(ActivePlayer playerKilled);
    public event PlayerDeath OnPlayerDeath;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private PlayerManager _manager;
    [SerializeField] private GameObject _playerMesh, _playerUI;
    private ActivePlayer _activePlayer;
    private int _currentHealth;
    void Start()
    {
        _activePlayer = GetComponent<ActivePlayer>();
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
        OnPlayerDeath?.Invoke(_activePlayer);
        _playerMesh.gameObject.SetActive(false);
        _playerUI.SetActive(false);
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
