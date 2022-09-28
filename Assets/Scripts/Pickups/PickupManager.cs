using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupManager : MonoBehaviour
{
    private static PickupManager _instance;
    [SerializeField] private List<GameObject> _pickupPrefabs;
    [SerializeField] private List<Transform> _pickupLocations;
    private int _defaultChance;
    private int _chanceToSpawn;

    public delegate void SpawnAction();
    public event SpawnAction OnSpawned;
    [SerializeField] private TextMeshProUGUI _text;

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
    private IEnumerator Spawn()
    {
        Transform spawnLocation = _pickupLocations[Random.Range(0, _pickupLocations.Count)];
        GameObject spawnedPrefab = _pickupPrefabs[Random.Range(0, _pickupPrefabs.Count)];
        GameObject newPrefab = Instantiate(spawnedPrefab, spawnLocation.position, Quaternion.identity);
        _text.gameObject.SetActive(true);
        _text.text = spawnedPrefab.name + " has spawned at " + spawnLocation.name;
        yield return new WaitForSeconds(4f);
        _text.gameObject.SetActive(false);

    }
    public void TryToSpawn()
    {
        int randomValue = Random.Range(1, _chanceToSpawn);
        if(randomValue <= 20)
        {
            StartCoroutine(Spawn());
            _chanceToSpawn = _defaultChance;
        }
        else
        {
            Debug.Log("Random value was: " + randomValue + ", no spawn");
            _chanceToSpawn -= 20;
        }
    }
}
