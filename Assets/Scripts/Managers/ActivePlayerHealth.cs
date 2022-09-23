using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivePlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = 1;
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

    }
}
