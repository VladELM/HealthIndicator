using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.MaxValueAssigned += OnMaxHealthValueAssigned;
        Health.ValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        Health.MaxValueAssigned -= OnMaxHealthValueAssigned;
        Health.ValueChanged -= OnHealthValueChanged;
    }
    protected abstract void OnMaxHealthValueAssigned(float value);
    protected abstract void OnHealthValueChanged(float value);
}
