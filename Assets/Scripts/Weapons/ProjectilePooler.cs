using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler PoolInstance;
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;

    private void Awake()
    {
        PoolInstance = this;
    }
    private void Start()
    {
        _pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_objectToPool);
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}