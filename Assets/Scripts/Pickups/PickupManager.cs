using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static PickupManager _instance;
    [SerializeField] private List<GameObject> _pickupPrefabs;
    [SerializeField] private List<Transform> _pickupLocations;
    private int _defaultChance;
    private int _chanceToSpawn;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _defaultChance = 100;
        _chanceToSpawn = _defaultChance;
    }
    public static PickupManager GetInstance()
    {
        return _instance;
    }
    void Spawn()
    {
        Transform spawnLocation = _pickupLocations[Random.Range(0, _pickupLocations.Count)];
        GameObject spawnedPrefab = _pickupPrefabs[Random.Range(0, _pickupPrefabs.Count)];
        GameObject newPrefab = Instantiate(spawnedPrefab, spawnLocation.position, Quaternion.identity);
    }
    public void TryToSpawn()
    {
        int randomValue = Random.Range(1, _chanceToSpawn);
        if(randomValue <= 20)
        {
            Spawn();
            _chanceToSpawn = _defaultChance;
            Debug.Log("Spawn");
        }
        else
        {
            Debug.Log("Random value was: " + randomValue + ", no spawn");
            _chanceToSpawn -= 20;
        }
    }
}
