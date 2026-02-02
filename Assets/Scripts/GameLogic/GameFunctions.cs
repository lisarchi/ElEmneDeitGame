using UnityEngine;

public class GameFunctions : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void PausedGame()
    {
        Time.timeScale = 0f;
        AutoSave();
    }

    public void ExitGame()
    {
        AutoSave();
        Debug.Log("Bye");
        Application.Quit();
    }

    private void AutoSave()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Игрок не найден, сохранение пропущено");
            return;
        }

        HealthSaveAdapter healthAdapter = player.GetComponent<HealthSaveAdapter>();
        if (healthAdapter == null)
        {
            Debug.LogWarning("HealthSaveAdapter не найден на игроке");
            return;
        }

        int lastCheckpointId = 0;

        if (CheckpointManager.Instance != null)
        {
            lastCheckpointId = CheckpointManager.Instance.LastCheckpointId;
        }
        else
        {
            Debug.LogWarning("CheckpointManager не найден, сохранение позиции пропущено");
        }

        SaveData data = new SaveData
        {
            health = healthAdapter.GetHealth(),
            checkpointID = lastCheckpointId
        };

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveGame(data);
            Debug.Log("Автосохранение выполнено!");
        }
        else
        {
            Debug.LogWarning("SaveManager не найден, автосохранение не выполнено");
        }
    }
}
