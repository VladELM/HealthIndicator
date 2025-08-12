using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Damager : MonoBehaviour
{
    [SerializeField] private Button _damageButton;
    [SerializeField] private int _damage;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _damageButton.onClick.AddListener(Damage);
    }

    private void OnDisable()
    {
        _damageButton?.onClick.RemoveListener(Damage);
    }

    public void Damage()
    {
        _health.TakeDamage(_damage);
    }
}
