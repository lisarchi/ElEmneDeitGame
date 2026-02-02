using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerController _controller;
    public Transform respawnPoint;
    public int checkpointID;
    private Collider2D coll;

    private void Awake()
    {
        // Находим игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _controller = player.GetComponent<PlayerController>();

        coll = GetComponent<Collider2D>();
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

        if (coll != null)
            coll.enabled = false;
    }
}
