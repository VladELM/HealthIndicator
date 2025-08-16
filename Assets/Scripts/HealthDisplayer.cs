using System;
using System.Collections;
using UnityEngine;

public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _healingValue;
    [SerializeField] private int _damagingValue;
    [SerializeField] private float _healingTime;
    [SerializeField] private float _damagingTime;

    private int _currentValue;
    private int _targetHealth;
    private Coroutine _healCoroutine;
    private Coroutine _damageCoroutine;
    private WaitForSeconds _healingDelay;
    private WaitForSeconds _damagingDelay;
    private bool _isHealing;
    private bool _isDamaging;

    public event Action<int> HealthChanged;
    public event Action Alived;
    public event Action Dead;

    private void Awake()
    {
        _health.HealthHealed += StartHealing;
        _health.HealthDamaged += StartDamaging;
        _healingDelay = new WaitForSeconds(_healingTime);
        _damagingDelay = new WaitForSeconds(_damagingTime);
        _isHealing = false;
        _isDamaging = false;
    }

    private void OnEnable()
    {
        _health.HealthHealed += StartHealing;
        _health.HealthDamaged += StartDamaging;
    }

    private void OnDisable()
    {
        _health.HealthHealed -= StartHealing;
        _health.HealthHealed -= StartDamaging;
    }

    public void StartHealing(int currentValue, int targetHealth)
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

        _currentValue = currentValue;
        _targetHealth = targetHealth;
        _healCoroutine = StartCoroutine(Healing());
    }

    public void StartDamaging(int currentValue, int targetHealth)
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

        _currentValue = currentValue;
        _targetHealth = targetHealth;
        _damageCoroutine = StartCoroutine(Damaging());
    }

    private IEnumerator Healing()
    {
        while (enabled)
        {
            yield return _healingDelay;

            if (_currentValue == 0)
                Alived?.Invoke();

            _currentValue += _healingValue;
            HealthChanged?.Invoke(_currentValue);

            if (_currentValue == _targetHealth)
                break;
        }
    }

    private IEnumerator Damaging()
    {
        while (enabled)
        {
            yield return _damagingDelay;

            _currentValue -= _damagingValue;
            HealthChanged?.Invoke(_currentValue);

            if (_currentValue == 0)
                Dead?.Invoke();

            if (_currentValue == _targetHealth)
                break;
        }
    }
}
