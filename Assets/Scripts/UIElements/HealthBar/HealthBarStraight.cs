public class HealthBarStraight : HealthBar
{
    private void OnEnable()
    {
        _health.MaxHealthAssigned += SetStartHealthValue;
        _health.HealthChanged += SetHealthValue;
        _health.Alived += ToggleFillArea;
        _health.Dead += ToggleFillArea;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= SetStartHealthValue;
        _health.HealthChanged -= SetHealthValue;
        _health.Alived -= ToggleFillArea;
        _health.Dead -= ToggleFillArea;
    }
}
