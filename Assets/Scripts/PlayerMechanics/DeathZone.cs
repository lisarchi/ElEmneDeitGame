using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject GameFunctions;
    private DeathEvent _deathEvent;

    private void Awake()
    {
        _deathEvent = GameFunctions.GetComponent<DeathEvent>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            print("death");
            _deathEvent.Death();
        }
    }
}
