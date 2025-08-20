using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public abstract class HealthChanger : MonoBehaviour
{
    [SerializeField] private Button _button;

    protected Health Health;

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClickButton);
    }

    protected abstract void HandleClickButton();
}
