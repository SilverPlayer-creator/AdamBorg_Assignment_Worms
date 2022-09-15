using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static PickupManager _instance;
    [SerializeField] private GameObject _pickupPrefab;

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
    }

    public static PickupManager GetInstance()
    {
        return _instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newPickup = Instantiate(_pickupPrefab);
            newPickup.transform.position = new Vector3(Random.Range(0f, 5f), 2f, Random.Range(0f, 5f));
        }
    }
}
