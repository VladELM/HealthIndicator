using UnityEngine;

public class HealthTextSmooth : HealthText
{
    [SerializeField] private HealthDisplayer _healthDisplayer;

    private void OnEnable()
    {
        _health.MaxHealthAssigned += SetTextPattern;
        _healthDisplayer.HealthChanged += SetText;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= SetTextPattern;
        _healthDisplayer.HealthChanged -= SetText;
    }
}
