using UnityEngine;

public class AutoSave : MonoBehaviour
{
    private void OnApplicationPause(bool pause)
    {
        if (!pause) return;

        SaveData data = new SaveData
        {
            //health = PlayerHealth.Instance.CurrentHealth,
            //checkpointId = PlayerCheckpointTracker.Instance.CurrentCheckpointId
        };

        SaveSystem.Save(data);
    }
}
