using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public int LastCheckpointId { get; private set; } = 0;

    private Dictionary<int, Transform> _checkpointMap = new Dictionary<int, Transform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (SaveManager.Instance != null && SaveManager.Instance.HasSave())
        {
            SaveData data = SaveManager.Instance.LoadGame();
            if (data != null)
                LastCheckpointId = data.checkpointID;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CollectCheckpoints();
    }

    private void CollectCheckpoints()
    {
        _checkpointMap.Clear();

        GameObject[] checkpointObjects =
            GameObject.FindGameObjectsWithTag("Checkpoint");

        foreach (GameObject obj in checkpointObjects)
        {
            Checkpoint cp = obj.GetComponent<Checkpoint>();

            if (cp == null)
            {
                Debug.LogWarning(
                    $"Объект {obj.name} имеет тег Checkpoint, но без компонента Checkpoint");
                continue;
            }

            if (_checkpointMap.ContainsKey(cp.checkpointID))
            {
                Debug.LogError($"Дубликат checkpointID: {cp.checkpointID}", cp);
                continue;
            }

            _checkpointMap.Add(cp.checkpointID, cp.respawnPoint);
        }

        Debug.Log($"CheckpointManager: найдено чекпоинтов {_checkpointMap.Count}");
    }

    public void RegisterCheckpoint(int checkpointId)
    {
        if (!_checkpointMap.ContainsKey(checkpointId))
        {
            Debug.LogWarning(
                $"Попытка зарегистрировать несуществующий чекпоинт: {checkpointId}");
            return;
        }

        LastCheckpointId = checkpointId;

        if (SaveManager.Instance != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var healthAdapter = player != null
                ? player.GetComponent<HealthSaveAdapter>()
                : null;

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
        _checkpointMap.TryGetValue(id, out Transform point);
        return point;
    }

    public void ResetProgress()
    {
        LastCheckpointId = 0;
        _checkpointMap.Clear();
    }

    public Vector2 GetLastCheckpointPosition()
    {
        Transform cp = GetCheckpoint(LastCheckpointId);
        return cp != null ? (Vector2)cp.position : Vector2.zero;
    }

    public bool HasCheckpoints()
    {
        return _checkpointMap.Count > 0;
    }
}
