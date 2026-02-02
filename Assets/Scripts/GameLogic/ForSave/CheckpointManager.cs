using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField] private Checkpoint[] checkpoints;
    private Dictionary<int, Transform> checkpointMap = new Dictionary<int, Transform>();

    public int LastCheckpointId { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        checkpointMap.Clear();
        foreach (var cp in checkpoints)
        {
            if (cp == null) continue;
            if (checkpointMap.ContainsKey(cp.checkpointID))
            {
                Debug.LogError($"Дубликат checkpointID: {cp.checkpointID}", cp);
                continue;
            }
            checkpointMap.Add(cp.checkpointID, cp.transform);
        }

        if (SaveManager.Instance != null && SaveManager.Instance.HasSave())
        {
            SaveData data = SaveManager.Instance.LoadGame();
            if (data != null)
                LastCheckpointId = data.checkpointID;
        }
    }

    public void RegisterCheckpoint(int checkpointId)
    {
        if (!checkpointMap.ContainsKey(checkpointId))
        {
            Debug.LogWarning($"Попытка зарегистрировать несуществующий чекпоинт: {checkpointId}");
            return;
        }

        LastCheckpointId = checkpointId;


        if (SaveManager.Instance != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var healthAdapter = player != null ? player.GetComponent<HealthSaveAdapter>() : null;

            SaveData data = new SaveData
            {
                checkpointID = checkpointId,
                health = healthAdapter != null ? healthAdapter.GetHealth() : 5
            };

            SaveManager.Instance.SaveGame(data);
        }
    }

    public Transform GetCheckpoint(int id)
    {
        checkpointMap.TryGetValue(id, out Transform point);
        return point;
    }

    public Vector2 GetLastCheckpointPosition()
    {
        Transform cp = GetCheckpoint(LastCheckpointId);
        return cp != null ? (Vector2)cp.position : Vector2.zero;
    }
}
