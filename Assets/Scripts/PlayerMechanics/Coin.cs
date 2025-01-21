using UnityEngine;

public class Coin : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerHealth>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            _playerHealth.HillHP();
            Destroy(gameObject);
        }
    }
}
