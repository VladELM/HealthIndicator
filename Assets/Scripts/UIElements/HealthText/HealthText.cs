using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthText : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private TMP_Text _textMeshPro;
    private string _textPattern;

    private void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _health.MaxHealthAssigned += SetTextPattern;
        _health.HealthChanged += SetText;
    }

    private void OnDisable()
    {
        _health.MaxHealthAssigned -= SetTextPattern;
        _health.HealthChanged -= SetText;
    }

    protected void SetTextPattern(float maxValue)
    {
        _textPattern = " / " + maxValue.ToString();
        SetText(maxValue);
    }

    protected void SetText(float value)
    {
        _textMeshPro.text = Convert.ToInt32(value) + _textPattern;
    }
}
