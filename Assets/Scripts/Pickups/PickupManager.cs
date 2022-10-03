using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PickupManager : MonoBehaviour
{
    public delegate void PickupEvent(Vector3 pos);
    public event PickupEvent OnPickup;
    [SerializeField] private List<Transform> _transforms;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<Pickup> _pickups;
    private static PickupManager _instance;
    private Dictionary<Vector3, bool> _pickupDict = new Dictionary<Vector3, bool>();
    private int _defaultChance;
    private int _chanceToSpawn;


    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        _defaultChance = 100;
        _chanceToSpawn = _defaultChance;
        OnPickup += EnableLocation;
        foreach (Transform transform in _transforms)
            _pickupDict.Add(transform.position, true);
        foreach (var item in _pickupDict)
            Debug.Log(item.Key + " " + item.Value);
        TurnManager.TurnInstance.OnTurnEnding += TryToSpawn;
    }
    public static PickupManager GetInstance => _instance;
    private void Spawn()
    {
            Vector3 newLocation = Vector3.zero;
            Transform spawnLocation;
            int loopCount = 0;
            while (true)
            {               
                int randomInt = Random.Range(0, _pickupDict.Count);
                spawnLocation = _transforms[randomInt];
                newLocation = _pickupDict.Keys.ElementAt(randomInt);
                if (loopCount > 30)
                    return;
                if (_pickupDict[newLocation] == true)
                    break;
                loopCount++;
            }
            Pickup spawnedPrefab = _pickups[Random.Range(0, _pickups.Count)];
            Pickup newPrefab = Instantiate(spawnedPrefab, newLocation, Quaternion.identity);
            StartCoroutine(SpawnText(spawnedPrefab.name, spawnLocation.name));
            _pickupDict[newLocation] = false;
    }
    private IEnumerator SpawnText(string prefabName, string locationName)
    {
        _text.gameObject.SetActive(true);
        _text.text = prefabName + " has spawned at " + locationName;
        yield return new WaitForSeconds(4f);
        _text.gameObject.SetActive(false);
    }
    public void TryToSpawn(bool _bool)
    {
        int randomValue = Random.Range(1, _chanceToSpawn);
        if(randomValue <= 20)
        {
            Spawn();
            _chanceToSpawn = _defaultChance;
        }
        else
        {
            Debug.Log("Random value was: " + randomValue + ", no spawn");
            _chanceToSpawn -= 20;
        }
    }
    public void InvokePickup(Vector3 pos)
    {
        OnPickup?.Invoke(pos);
    }
    void EnableLocation(Vector3 pos)
    {
        if (_pickupDict.ContainsKey(pos))
            _pickupDict[pos] = true;
    }
    private void OnDestroy()
    {
        TurnManager.TurnInstance.OnTurnEnding -= TryToSpawn;
    }
}
