using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthBarText : MonoBehaviour
{
    private TMP_Text _textMeshPro;
    private string _textPattern;

    private void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
    }

    public void SetTextPattern(int maxValue)
    {
        _textPattern = " / " + maxValue.ToString();
        SetText(maxValue);
    }

    public void SetText(int value)
    {
        _textMeshPro.text = value + _textPattern;
    }
}
