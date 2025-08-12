using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int _healthValue;

    public event Action<int> HealthChanged;
    public event Action<int> MaxHealthAssigned;
    public event Action HealthStarted;
    public event Action Alived;
    public event Action Dead;

    public int MaxHealthValue {get; private set;}

    private void Awake()
    {
        MaxHealthValue = _healthValue;
    }

    private void Start()
    {
        MaxHealthAssigned?.Invoke(MaxHealthValue);
        HealthStarted?.Invoke();
    }

    public void Heal(int healPoints)
    {
        if (_healthValue < MaxHealthValue)
        {
            if ((_healthValue + healPoints) <= MaxHealthValue)
            {
                if (_healthValue == 0)
                    Alived?.Invoke();

                _healthValue += healPoints;
            }
            else if (_healthValue + healPoints > MaxHealthValue)
            {
                _healthValue = MaxHealthValue;
            }

            HealthChanged?.Invoke(_healthValue);
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
                    _healthValue -= incomingDamage;
                    HealthChanged?.Invoke(_healthValue);
                }
                else if (_healthValue < incomingDamage)
                {
                    _healthValue = 0;
                    HealthChanged?.Invoke(_healthValue);
                }

                if (_healthValue == 0)
                    Dead?.Invoke();
            }
        }
    }
}
