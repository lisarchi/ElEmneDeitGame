using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BagSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;                 
    [SerializeField] private GameObject[] _bagPrefabs;     
    [SerializeField] private float _spawnDistance = 25f;        
    [SerializeField] private float _despawnDistance = 30f;      

    [SerializeField] private float _minSpawnDelay = 5f;
    [SerializeField] private float _maxSpawnDelay = 15f;

    [SerializeField] private int _poolSizePerType = 4; 

    [SerializeField] private float _horizontalSpread = 0f;
    [SerializeField] private float _verticalSpread = 3f;   

    private List<GameObject> _pool = new List<GameObject>();

    private void Start()
    {
        CreatePool();
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (_player == null) return;

        foreach (GameObject obj in _pool)
        {
            if (obj.activeInHierarchy && Vector3.Distance(_player.position, obj.transform.position) > _despawnDistance)
            {
                obj.SetActive(false);
            }
        }
    }

    private void CreatePool()
    {
        foreach (GameObject prefab in _bagPrefabs)
        {
            for (int i = 0; i < _poolSizePerType; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float delay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            yield return new WaitForSeconds(delay);
            SpawnBag();
        }
    }

    private void SpawnBag()
    {
        if (_player == null) return;

        GameObject prefab = _bagPrefabs[Random.Range(0, _bagPrefabs.Length)];

        GameObject bag = _pool.Find(o => !o.activeInHierarchy && o.name.Contains(prefab.name));

        if (bag == null)
        {
            bag = Instantiate(prefab);
            _pool.Add(bag);
        }

        Vector3 basePos = _player.position + _player.right * _spawnDistance;

        Vector3 offset = new Vector2(
            Random.Range(-_horizontalSpread, _horizontalSpread),
            Random.Range(-_verticalSpread, _verticalSpread)
        );

        Vector3 spawnPos = basePos + _player.right * offset.x + _player.up * offset.y;

        bag.transform.position = spawnPos;
        bag.transform.rotation = Quaternion.identity;
        bag.SetActive(true);
    }
}
