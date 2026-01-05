using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] internal int _health;
    [SerializeField] internal int _maxHealth = 50;
    [SerializeField] internal HealthBar _healthBar;
    [SerializeField] private PlayerController _controller;
    private float _timeOfDamage;

    private int _coinHealth = 1;

    private void Start()
    {
        _health = 10;
        _healthBar.UpdateHealth(_maxHealth, _health);

    }
    internal void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (_health > _maxHealth)
        {
            _health = 50;
        }

        if (_health < 1)
        {
            _controller.Die();
            _health = 10; 
            _healthBar.UpdateHealth(_maxHealth, _health);
        }
    }

    internal void HillHP()
    {
        _health = _health + _coinHealth;

        _healthBar.UpdateHealth(_maxHealth, _health);
    }

    internal void Damage()
    {
        if (_timeOfDamage > 0)
        {
            _timeOfDamage -= Time.deltaTime;
        }
        if (_timeOfDamage <= 0)
        {
            _timeOfDamage = 0.5f;
            _health--;

            _healthBar.UpdateHealth(_maxHealth, _health);
        }
    }
}
