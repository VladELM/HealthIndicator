using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _fillArea;

    private Slider _slider;
    private bool _isFillAreaEnabled;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _isFillAreaEnabled = true;
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
