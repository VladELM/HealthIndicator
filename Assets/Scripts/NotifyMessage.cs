using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class NotifyMessage : MonoBehaviour
{
    [SerializeField] private string _deathString;
    [SerializeField] private string _aliveString;

    private TMP_Text _text;

    private bool _isAlive;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _isAlive = true;
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
