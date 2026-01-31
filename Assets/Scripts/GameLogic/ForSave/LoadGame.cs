using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private void Start()
    {
        if (!SaveSystem.HasSave()) return;

        SaveData data = SaveSystem.Load();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // здоровье
        GetComponent<PlayerHealth>()._healthBar.UpdateHealth(50, data.health);

        // позиция чекпоинта
        Transform checkpoint = CheckpointManager.Instance.GetCheckpointByID(data.checkpointID);

        if (checkpoint != null)
            player.transform.position = checkpoint.position;
    }
}
