using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Healler))]
[RequireComponent(typeof(Damager))]
public class Subscriber : MonoBehaviour
{
    [SerializeField] private Button _damageButton;
    [SerializeField] private Button _healButton;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private HealthBarText _healthBarText;
    [SerializeField] private NotifyMessage _notifyMessage;

    private Health _health;
    private Healler _healler;
    private Damager _damager;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _healler = GetComponent<Healler>();
        _damager = GetComponent<Damager>();
    }

    private void OnEnable()
    {
        _damageButton.onClick.AddListener(_damager.Damage);
        _healButton.onClick.AddListener(_healler.HealCharacter);

        _health.MaxHealthAssigned += _healthBar.SetStartHealthValue;
        _health.MaxHealthAssigned += _healthBarText.SetTextPattern;
        _health.HealthStarted += _notifyMessage.AssigneStartMessage;

        _health.HealthChanged += _healthBar.SetHealthValue;
        _health.HealthChanged += _healthBarText.SetText;

        _health.Alived += _healthBar.ToggleFillArea;
        _health.Alived += _notifyMessage.RewriteMessage;

        _health.Dead += _healthBar.ToggleFillArea;
        _health.Dead += _notifyMessage.RewriteMessage;
    }

    private void OnDisable()
    {
        _damageButton.onClick.RemoveListener(_damager.Damage);
        _healButton.onClick.RemoveListener(_healler.HealCharacter);

        _health.MaxHealthAssigned -= _healthBar.SetStartHealthValue;
        _health.MaxHealthAssigned -= _healthBarText.SetTextPattern;
        _health.HealthStarted -= _notifyMessage.AssigneStartMessage;

        _health.HealthChanged -= _healthBar.SetHealthValue;
        _health.HealthChanged -= _healthBarText.SetText;
        
        _health.Alived -= _healthBar.ToggleFillArea;
        _health.Alived += _notifyMessage.RewriteMessage;

        _health.Dead -= _healthBar.ToggleFillArea;
        _health.Dead -= _notifyMessage.RewriteMessage;
    }
}
