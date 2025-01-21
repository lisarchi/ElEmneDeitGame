using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] internal int _health;
    [SerializeField] internal int _maxHealth = 50;
    [SerializeField] private GameObject GameFunctions;
    [SerializeField] internal HealthBar _healthBar;

    private float _timeOfDamage;
    private DeathEvent _deathEvent;
    private int _coinHealth = 1;

    private void Awake()
    {
        _deathEvent = GameFunctions.GetComponent<DeathEvent>();
    }

    private void Start()
    {
        _health = 5;
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
            _deathEvent.Death();
        }
    }

    internal void HillHP()
    {
        _health = _health + _coinHealth;
        print(_health.ToString());
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
            print(_health.ToString());
            _healthBar.UpdateHealth(_maxHealth, _health);
        }
    }
}
