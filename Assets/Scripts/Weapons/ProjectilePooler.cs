using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
 [System.Serializable]   public class Pool
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private string _name;
        [SerializeField] private int _size;
    }
    [SerializeField] private List<Pool> _pools;
    //public static ProjectilePooler PoolInstance;
    //[SerializeField] private List<GameObject> _pooledObjects;
    //[SerializeField] private GameObject _objectToPool;
    //[SerializeField] private int _amountToPool;
    //[SerializeField] private Dictionary<string, Queue<GameObject>> _poolDictionary;
    private void Awake()
    {
        //PoolInstance = this;
    }
    //private void Start()
    //{
    //    _poolDictionary = new Dictionary<string, Queue<GameObject>>();
    //    _pooledObjects = new List<GameObject>();
    //    GameObject tmp;
    //    for (int i = 0; i < _amountToPool; i++)
    //    {
    //        tmp = Instantiate(_objectToPool);
    //        tmp.SetActive(false);
    //        _pooledObjects.Add(tmp);
    //    }
    //}

    //public GameObject GetPooledObject()
    //{
    //    for (int i = 0; i < _amountToPool; i++)
    //    {
    //        if (!_pooledObjects[i].activeInHierarchy)
    //        {
    //            return _pooledObjects[i];
    //        }
    //    }
    //    return null;
    //}
}
