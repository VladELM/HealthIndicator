using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _fillArea;
    [SerializeField] private Health _health;
    [SerializeField] private HealthDisplayer _healthDisplayer;

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
        _healthDisplayer.HealthChanged += SetHealthValue;
        _healthDisplayer.Alived += ToggleFillArea;
        _healthDisplayer.Dead += ToggleFillArea;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= SetStartHealthValue;
        _healthDisplayer.HealthChanged -= SetHealthValue;
        _healthDisplayer.Alived -= ToggleFillArea;
        _healthDisplayer.Dead -= ToggleFillArea;
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
