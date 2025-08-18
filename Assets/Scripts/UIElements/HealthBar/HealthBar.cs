using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health _health;
    [SerializeField] private Transform _fillArea;

    private Slider _slider;
    private bool _isFillAreaEnabled;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _isFillAreaEnabled = true;
    }

    protected void SetStartHealthValue(float healthMax)
    {
        _slider.wholeNumbers = true;
        _slider.maxValue = healthMax;
        _slider.value = healthMax;
    }

    protected void SetHealthValue(float health)
    {
        _slider.value = health;
    }

    protected void ToggleFillArea()
    {
        if (_isFillAreaEnabled)
            _fillArea.gameObject.SetActive(false);
        else
            _fillArea.gameObject.SetActive(true);

        _isFillAreaEnabled = !_isFillAreaEnabled;
    }
}
