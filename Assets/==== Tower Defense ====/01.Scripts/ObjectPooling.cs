using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    public int sizePool;
    private Dictionary<GameObject, List<GameObject>> pools = new();
    [SerializeField] List<GameObject> poolObj = new();
    GameObject _cache;
    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }
    private void CreatePool()
    {
        for(int i = 0; i < poolObj.Count; i++)
        {
            var obj = poolObj[i];
            if (!pools.ContainsKey(obj))
            {
                pools.Add(obj, new List<GameObject>());
                for(int j = 0; j < sizePool; j++)
                {
                    _cache = Instantiate(obj,transform);
                    _cache.SetActive(false);
                    pools[obj].Add(_cache);
                }
            }
        }
    }

    public GameObject GetObjFromPool(GameObject key)
    {
        if(!pools.ContainsKey(key))
        {
            Debug.LogError($"{key.name} is not exist in pool");
            return null;
        }
        for(int i=0; i < pools[key].Count; i++)
        {
            if (pools[key][i].activeInHierarchy)
                continue;
            else
                return pools[key][i];
        }
        _cache = Instantiate(key, transform);
        _cache.SetActive(false);
        pools[key].Add(_cache);
        return _cache;
    }
}
