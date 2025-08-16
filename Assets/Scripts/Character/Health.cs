using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] private int _healingValue;
    [SerializeField] private int _damagingValue;
    [SerializeField] private float _healingTime;
    [SerializeField] private float _damagingTime;

    public event Action<int, int> HealthHealed;
    public event Action<int, int> HealthDamaged;
    public event Action<int> MaxHealthAssigned;
    public event Action HealthStarted;

    public int MaxHealthValue {get; private set;}

    private void Awake()
    {
        MaxHealthValue = _health;
    }

    private void Start()
    {
        MaxHealthAssigned?.Invoke(MaxHealthValue);
        HealthStarted?.Invoke();
    }

    public void Heal(int healPoints)
    {
        if (_health < MaxHealthValue)
        {
            if ((_health + healPoints) <= MaxHealthValue)
            {
                HealthHealed?.Invoke(_health, _health + healPoints);
                _health += healPoints;
            }
            else if (_health + healPoints > MaxHealthValue)
            {
                HealthHealed?.Invoke(_health, MaxHealthValue);
                _health = MaxHealthValue;
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
                    HealthDamaged?.Invoke(_health, _health - incomingDamage);
                    _health -= incomingDamage;
                }
                else if (_health < incomingDamage)
                {
                    HealthDamaged?.Invoke(_health, 0);
                    _health = 0;
                }
            }
        }
    }
}
