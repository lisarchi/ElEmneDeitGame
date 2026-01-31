using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerController _controller;
    public Transform respawnPoint;
    public int checkpointID;
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

            PlayerHealth health = GetComponent<PlayerHealth>();

            SaveData data = new SaveData
            {
                health = health._health,
                checkpointID = checkpointID
            };

            SaveSystem.Save(data);
        }
    }
}
