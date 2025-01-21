using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _hpBar;
    internal void UpdateHealth(int _maxHealth, int _health)
    {
        float healthPercent = (float)_health / _maxHealth;

        _hpBar.fillAmount = healthPercent;
    }
}
