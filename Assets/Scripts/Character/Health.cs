using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float _healthValue;

    private float _maxHealthValue;

    public event Action<float> HealthChanged;
    public event Action<float> MaxHealthAssigned;
    public event Action HealthStarted;
    public event Action Alived;
    public event Action Dead;

    private void Awake()
    {
        _maxHealthValue = _healthValue;
    }

    private void Start()
    {
        MaxHealthAssigned?.Invoke(_maxHealthValue);
        HealthStarted?.Invoke();
    }

    public void Heal(int healPoints)
    {
        if (_healthValue < _maxHealthValue)
        {
            if (_healthValue == 0)
                Alived?.Invoke();

            float healthValue = _healthValue + healPoints;

            if (healthValue <= _maxHealthValue)
            {
                HealthChanged?.Invoke(_healthValue + healPoints);
                _healthValue += healPoints;
            }
            else if (healthValue > _maxHealthValue)
            {
                HealthChanged?.Invoke(_maxHealthValue);
                _healthValue = _maxHealthValue;
            }
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (incomingDamage > 0)
        {
            if (_healthValue != 0)
            {
                if (_healthValue >= incomingDamage)
                {
                    HealthChanged?.Invoke(_healthValue - incomingDamage);
                    _healthValue -= incomingDamage;
                }
                else if (_healthValue < incomingDamage)
                {
                    HealthChanged?.Invoke(0);
                    _healthValue = 0;
                }

                if (_healthValue == 0)
                    Dead?.Invoke();
            }
        }
    }
}
