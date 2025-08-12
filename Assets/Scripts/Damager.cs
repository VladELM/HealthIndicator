using UnityEngine;

[RequireComponent(typeof(Health))]
public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void Damage()
    {
        _health.TakeDamage(_damage);
    }
}
