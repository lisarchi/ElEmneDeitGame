using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerController _controller;
    public Transform respawnPoint;
    Collider2D coll;

    internal void Awake()
    {
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coll  = GetComponent<Collider2D>();
    }

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _controller.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
        }
    }
}
