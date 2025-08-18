using UnityEngine;

public class HealthBarSmooth : HealthBar
{
    [SerializeField] private HealthDisplayer _healthDisplayer;

    private void OnEnable()
    {
        _health.MaxHealthAssigned += SetStartHealthValue;
        _healthDisplayer.HealthChanged += SetHealthValue;
        _health.Alived += ToggleFillArea;
        _health.Dead += ToggleFillArea;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= SetStartHealthValue;
        _healthDisplayer.HealthChanged -= SetHealthValue;
        _health.Alived -= ToggleFillArea;
        _health.Dead -= ToggleFillArea;
    }
}
