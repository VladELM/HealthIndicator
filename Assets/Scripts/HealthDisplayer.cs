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

    private int _currentHealth;
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
        _healingDelay = new WaitForSeconds(_healingTime);
        _damagingDelay = new WaitForSeconds(_damagingTime);
        _isHealing = false;
        _isDamaging = false;
    }

    private void OnEnable()
    {
        _health.MaxHealthAssigned += AssignStartHealth;
        _health.HealthHealed += StartHealing;
        _health.HealthDamaged += StartDamaging;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= AssignStartHealth;
        _health.HealthHealed -= StartHealing;
        _health.HealthHealed -= StartDamaging;
    }

    private void AssignStartHealth(int startHealth)
    {
        _currentHealth = startHealth;
    }

    private void StartHealing(int targetHealth)
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

        _targetHealth = targetHealth;
        _healCoroutine = StartCoroutine(Healing());
    }

    private void StartDamaging(int targetHealth)
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

        _targetHealth = targetHealth;
        _damageCoroutine = StartCoroutine(Damaging());
    }

    private IEnumerator Healing()
    {
        while (enabled)
        {
            yield return _healingDelay;

            if (_currentHealth == 0)
                Alived?.Invoke();

            _currentHealth += _healingValue;
            HealthChanged?.Invoke(_currentHealth);

            if (_currentHealth == _targetHealth)
                break;
        }
    }

    private IEnumerator Damaging()
    {
        while (enabled)
        {
            yield return _damagingDelay;

            _currentHealth -= _damagingValue;
            HealthChanged?.Invoke(_currentHealth);

            if (_currentHealth == 0)
                Dead?.Invoke();

            if (_currentHealth == _targetHealth)
                break;
        }
    }
}
