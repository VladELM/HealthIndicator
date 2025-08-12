using UnityEngine;

[RequireComponent(typeof(Health))]
public class Healler : MonoBehaviour
{
    [SerializeField] private int _healPoints;
    
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void HealCharacter()
    {
        _health.Heal(_healPoints);
    }
}
