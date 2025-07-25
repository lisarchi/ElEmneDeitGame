using UnityEngine;

public class SpawnMessages : MonoBehaviour
{
    [SerializeField] internal GameObject[] spawnObjects;

    internal void SpawnMessage()
    {
        var objectIndex = Random.Range(0, spawnObjects.Length);
        GameObject newMessage = Instantiate(spawnObjects[objectIndex], new Vector3(0,0,0), Quaternion.identity);
    }
}
