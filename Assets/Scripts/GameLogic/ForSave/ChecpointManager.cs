using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField]private Checkpoint[] checkpoints;
    private Dictionary<int, Transform> dict = new();

    private void Awake()
    {
        foreach (var cp in checkpoints)
            dict.Add(cp.checkpointID, cp.transform);
    }

    public Transform GetCheckpointByID(int id)
    {
        foreach (var cp in checkpoints)
        {
            if (cp.checkpointID == id)
                return cp.transform;
        }
        return null;
    }
}
