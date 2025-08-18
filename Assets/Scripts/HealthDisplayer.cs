using System;
using System.Collections;
using UnityEngine;

public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _changingTime;
    [SerializeField] private float _changingValue;

    private bool _isChanging;
    private float _currentHealth;
    private float _targetHealth;
    private WaitForSeconds _changingDelay;
    private Coroutine _coroutine;

    public event Action<float> HealthChanged;

    private void Awake()
    {
        _changingDelay = new WaitForSeconds(_changingTime);
        _isChanging = false;
    }

    private void OnEnable()
    {
        _health.MaxHealthAssigned += AssignStartHealth;
        _health.HealthChanged += StartChangeHealth;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= AssignStartHealth;
        _health.HealthChanged -= StartChangeHealth;
    }

    private void AssignStartHealth(float startHealth)
    {
        _currentHealth = startHealth;
    }

    private void StartChangeHealth(float targetHealth)
    {
        if (_isChanging)
        {
            _isChanging = false;
            StopCoroutine(_coroutine);
        }

        _targetHealth = targetHealth;
        _isChanging = true;
        _coroutine = StartCoroutine(HealthChanging(_changingDelay));
    }

    private IEnumerator HealthChanging(WaitForSeconds delay)
    {
        while (enabled)
        {
            yield return delay;

            _currentHealth = Mathf.MoveTowards(_currentHealth, _targetHealth, _changingValue * Time.deltaTime);
            HealthChanged?.Invoke(_currentHealth);

            if (_currentHealth == _targetHealth)
                break;
        }
    }
}
