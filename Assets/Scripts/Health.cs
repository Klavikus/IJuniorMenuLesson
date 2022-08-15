using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private float _currentHealth;

    public event Action<float> HealthChanged;

    public float HealthPercentage => _currentHealth / _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(HealthPercentage);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < 0)
            _currentHealth = 0;

        HealthChanged?.Invoke(HealthPercentage);
    }

    public void ApplyHeal(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        HealthChanged?.Invoke(HealthPercentage);
    }
}