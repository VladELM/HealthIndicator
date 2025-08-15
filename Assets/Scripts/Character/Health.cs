using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] private int _healingValue;
    [SerializeField] private int _damagingValue;
    [SerializeField] private float _healingTime;
    [SerializeField] private float _damagingTime;
    
    private int _targetHealth;
    private Coroutine _healCoroutine;
    private Coroutine _damageCoroutine;
    private WaitForSeconds _healingDelay;
    private WaitForSeconds _damagingDelay;
    private bool _isHealing;
    private bool _isDamaging;

    public event Action<int> HealthChanged;
    public event Action<int> MaxHealthAssigned;
    public event Action HealthStarted;
    public event Action Alived;
    public event Action Dead;

    public int MaxHealthValue {get; private set;}

    private void Awake()
    {
        MaxHealthValue = _health;
        _healingDelay = new WaitForSeconds(_healingTime);
        _damagingDelay = new WaitForSeconds(_damagingTime);
        _isHealing = false;
        _isDamaging = false;
    }

    private void Start()
    {
        MaxHealthAssigned?.Invoke(MaxHealthValue);
        HealthStarted?.Invoke();
    }

    public void Heal(int healPoints)
    {

        if (_isHealing)
            StopCoroutine(_healCoroutine);

        if (_isDamaging)
        {
            _isDamaging = false;
            StopCoroutine(_damageCoroutine);
        }

        if (_isHealing == false)
            _isHealing = true;

        AssignHealParameters(healPoints);
        _healCoroutine = StartCoroutine(Healing());
    }

    public void TakeDamage(int incomingDamage)
    {
        if (_isHealing)
        {
            _isHealing = false;
            StopCoroutine(_healCoroutine);
        }

        if (_isDamaging)
            StopCoroutine(_damageCoroutine);

        if (_isDamaging == false)
            _isDamaging = true;

        AssignDamageParameters(incomingDamage);
        _damageCoroutine = StartCoroutine(Damaging());
    }

    private void AssignHealParameters(int healPoints)
    {
        if (_health < MaxHealthValue)
        {
            if (_health + healPoints <= MaxHealthValue)
                _targetHealth = _health + healPoints;
            else if (_health + healPoints > MaxHealthValue)
                _targetHealth = MaxHealthValue;
        }
    }

    private void AssignDamageParameters(int incomingDamage)
    {
        if (incomingDamage > 0)
        {
            if (_health != 0)
            {
                if (_health >= incomingDamage)
                    _targetHealth = _health - incomingDamage;
                else if (_health < incomingDamage)
                    _targetHealth = 0;
            }
        }
    }

    private IEnumerator Healing()
    {
        while (enabled)
        {
            yield return _healingDelay;

            if (_health == 0)
                Alived?.Invoke();

            _health += _healingValue;
            HealthChanged?.Invoke(_health);

            if (_health == _targetHealth)
                break;
        }
    }

    private IEnumerator Damaging()
    {
        while (enabled)
        {
            yield return _damagingDelay;

            _health -= _damagingValue;
            HealthChanged?.Invoke(_health);

            if (_health == 0)
                Dead?.Invoke();

            if (_health == _targetHealth)
                break;
        }
    }
}
