using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Healler : MonoBehaviour
{
    [SerializeField] private Button _healButton;
    [SerializeField] private int _healPoints;
    
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _healButton.onClick.AddListener(HealCharacter);
    }

    private void OnDisable()
    {
        _healButton?.onClick.RemoveListener(HealCharacter);
    }

    public void HealCharacter()
    {
        _health.Heal(_healPoints);
    }
}
