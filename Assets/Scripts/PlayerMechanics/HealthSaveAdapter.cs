using UnityEngine;

public class HealthSaveAdapter : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    public int GetHealth()
    {
        return _playerHealth._health;
    }

    public void SetHealth(int value)
    {
        // защита от кривых данных
        value = Mathf.Clamp(value, 1,  _playerHealth._maxHealth);

        _playerHealth._health = value;

        // обновляем UI вручную
        _playerHealth._healthBar.UpdateHealth(
            _playerHealth._maxHealth,
            _playerHealth._health
        );
    }
}
