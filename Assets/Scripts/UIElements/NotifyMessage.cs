using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class NotifyMessage : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private HealthDisplayer _healthDisplayer;
    [SerializeField] private string _deathString;
    [SerializeField] private string _aliveString;

    private TMP_Text _text;

    private bool _isAlive;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _isAlive = true;
    }

    private void OnEnable()
    {
        _health.HealthStarted += AssigneStartMessage;
        _healthDisplayer.Alived += RewriteMessage;
        _healthDisplayer.Dead += RewriteMessage;
    }

    private void OnDisable()
    {
        _health.HealthStarted -= AssigneStartMessage;
        _healthDisplayer.Alived -= RewriteMessage;
        _healthDisplayer.Dead -= RewriteMessage;
    }

    public void AssigneStartMessage()
    {
        _text.text = _aliveString;
    }

    public void RewriteMessage()
    {
        if (_isAlive)
            _text.text = _deathString;
        else
            _text.text = _aliveString;

        _isAlive = !_isAlive;
    }
}
