using UnityEngine;

[RequireComponent(typeof(Health))]
public class Healler : HealthChanger
{
    [SerializeField] private int _healPoints;

    protected override void HandleClickButton()
    {
        Health.Heal(_healPoints);
    }
}
