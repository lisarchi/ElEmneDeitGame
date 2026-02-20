using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;
    public int checkpointID;

    private Collider2D _coll;
    private PlayerController _controller;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _controller = player.GetComponent<PlayerController>();

        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (_controller != null)
            _controller.UpdateCheckpoint(respawnPoint.position, checkpointID);

        HealthSaveAdapter adapter = collision.GetComponent<HealthSaveAdapter>();
        if (adapter != null && SaveManager.Instance != null)
        {
            SaveData data = new SaveData
            {
                health = adapter.GetHealth(),
                checkpointID = checkpointID
            };
            SaveManager.Instance.SaveGame(data);
            Debug.Log($"Сохранено на чекпоинте {checkpointID}");
        }

        if (_coll != null)
            _coll.enabled = false;
    }
}
