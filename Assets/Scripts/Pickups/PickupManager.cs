using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupManager : MonoBehaviour
{
    private static PickupManager _instance;
    [SerializeField] private List<Transform> _transforms;
    private Dictionary<Transform, bool> _pickupDict = new Dictionary<Transform, bool>();
    private int _defaultChance;
    private int _chanceToSpawn;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<Pickup> _pickups;
    public delegate void PickupEvent(Vector3 pos, string name);
    public event PickupEvent OnPickup;

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
        OnPickup += AddLocationToList;
        foreach (Transform transform in _transforms)
        {
            _pickupDict.Add(transform, true);
        }
        foreach (var item in _pickupDict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }
    public static PickupManager GetInstance()
    {
        return _instance;
    }
    private IEnumerator Spawn()
    {
        if(_transforms.Count != 0)
        {
            Transform spawnLocation = _transforms[Random.Range(0, _transforms.Count)];
            Pickup spawnedPrefab = _pickups[Random.Range(0, _pickups.Count)];
            Pickup newPrefab = Instantiate(spawnedPrefab, spawnLocation.position, Quaternion.identity);
            _text.gameObject.SetActive(true);
            _text.text = spawnedPrefab.name + " has spawned at " + spawnLocation.name;
            newPrefab.GetName(spawnLocation.name);
            _pickupDict.Remove(spawnLocation);
            yield return new WaitForSeconds(4f);
            _text.gameObject.SetActive(false);
        }
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
    public void AddLocationToList(Vector3 pos, string name)
    {
        
    }
    public void InvokePickup(Vector3 pos, string name)
    {
        OnPickup?.Invoke(pos, name);
    }
}
