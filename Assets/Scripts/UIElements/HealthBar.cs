using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _fillArea;
    [SerializeField] private Health _health;

    private Slider _slider;
    private bool _isFillAreaEnabled;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _isFillAreaEnabled = true;
    }

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

    public void SetStartHealthValue(int healthMax)
    {
        _slider.wholeNumbers = true;
        _slider.maxValue = healthMax;
        _slider.value = healthMax;
    }

    public void SetHealthValue(int health)
    {
        _slider.value = health;
    }

    public void ToggleFillArea()
    {
        if (_isFillAreaEnabled)
            _fillArea.gameObject.SetActive(false);
        else
            _fillArea.gameObject.SetActive(true);

        _isFillAreaEnabled = !_isFillAreaEnabled;
    }
}
