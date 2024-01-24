using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDistance;
    private List<GameObject> objectList = new List<GameObject>();
    private float _spawnTimer;

    private void Update()
    {
        if (_spawnTimer <= 0)
        {
            Spawn();
            _spawnTimer = spawnTime;
        }
        else
            _spawnTimer -= Time.deltaTime;
    }

    private void Spawn()
    {
        int selection = Random.Range(0, objectPrefabs.Count);
        objectList.RemoveAll(s => s == null);
        objectList.Add(Instantiate(objectPrefabs[selection], transform.position + Vector3.right * Random.Range(-spawnDistance, spawnDistance), Quaternion.identity));
    }

    public int GetObjectCount()
    {
        return objectList.Count;
    }
}
