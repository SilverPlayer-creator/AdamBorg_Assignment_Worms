using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
 [System.Serializable]   public class Pool
    {
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab { get { return _prefab; } }
        [SerializeField] private string _tag;
        public string Tag { get { return _tag; } }
        [SerializeField] private int _size;
        public int Size { get { return _size; } }
    }
    public static ProjectilePooler PoolInstance;
    [SerializeField] private List<Pool> _pools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> _poolDictionary;
    private void Awake()
    {
        PoolInstance = this;
    }
    private void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in _pools)
        {
            GameObject poolParent = new GameObject();
            poolParent.name = pool.Tag;
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.parent = poolParent.transform;
            }
            _poolDictionary.Add(pool.Tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rotation;
        _poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
