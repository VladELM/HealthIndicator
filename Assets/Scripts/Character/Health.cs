using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] private int _healingValue;
    [SerializeField] private int _damagingValue;
    [SerializeField] private float _healingTime;
    [SerializeField] private float _damagingTime;

    private int _maxHealthValue;

    public event Action<int> HealthHealed;
    public event Action<int> HealthDamaged;
    public event Action<int> MaxHealthAssigned;
    public event Action HealthStarted;

    private void Awake()
    {
        _maxHealthValue = _health;
    }

    private void Start()
    {
        MaxHealthAssigned?.Invoke(_maxHealthValue);
        HealthStarted?.Invoke();
    }

    public void Heal(int healPoints)
    {
        if (_health < _maxHealthValue)
        {
            if ((_health + healPoints) <= _maxHealthValue)
            {
                HealthHealed?.Invoke(_health + healPoints);
                _health += healPoints;
            }
            else if (_health + healPoints > _maxHealthValue)
            {
                HealthHealed?.Invoke(_maxHealthValue);
                _health = _maxHealthValue;
            }
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (incomingDamage > 0)
        {
            if (_health != 0)
            {
                if (_health >= incomingDamage)
                {
                    HealthDamaged?.Invoke(_health - incomingDamage);
                    _health -= incomingDamage;
                }
                else if (_health < incomingDamage)
                {
                    HealthDamaged?.Invoke(0);
                    _health = 0;
                }
            }
        }
    }
}
