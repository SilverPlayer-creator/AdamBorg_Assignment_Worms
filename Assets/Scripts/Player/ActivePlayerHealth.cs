using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ActivePlayerHealth : MonoBehaviour, IDamageable
{
    public delegate void PlayerDeath(ActivePlayer playerKilled);
    public event PlayerDeath OnPlayerDeath;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private PlayerManager _manager;
    [SerializeField] private GameObject _playerMesh, _playerUI;
    [SerializeField] private GameObject _deathEffect;
    private ActivePlayer _activePlayer;
    private int _currentHealth;
    private bool _hasDied;
    void Start()
    {
        _activePlayer = GetComponent<ActivePlayer>();
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = 1;
        _currentHealth = _maxHealth;
        PlayerManager.Instance.OnGameEnded += DisableUi;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.fillAmount = (float)_currentHealth / (float)_maxHealth;
        if(_currentHealth <= 0 && !_hasDied)
        {
            Die();
        }
    }
    void Die()
    {
        OnPlayerDeath?.Invoke(_activePlayer);
        GetComponent<PlayerHeldWeapons>().DisableInput();
        GameObject deathEffect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        _playerMesh.gameObject.SetActive(false);
        _playerUI.SetActive(false);
        _hasDied = true;
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
    void DisableUi()
    {
        if (_playerUI.activeSelf)
            _playerUI.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        PlayerManager.Instance.OnGameEnded -= DisableUi;
    }
    public bool HasDied => _hasDied;
}
