using UnityEngine;

[RequireComponent(typeof(Health))]
public class Damager : HealthChanger
{
    [SerializeField] private int _damage;

    protected override void HandleClickButton()
    {
        Health.TakeDamage(_damage);
    }
}
