using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    protected override void Awake()
    {
        base.Awake();

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-3.0f, 3.0f));
                GameObject obj = Instantiate(pool.prefab, pos, Quaternion.identity, this.transform);
                obj.SetActive(true);//юс╫ц
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        if (!poolDictionary[tag].TryDequeue(out GameObject obj))
        {
            Pool pool = FindPoolByTag(tag);
            obj = Instantiate(pool.prefab, transform);
            poolDictionary[tag].Enqueue(obj);
            obj.SetActive(false);
        }
        else if (obj.activeInHierarchy)
        {
            poolDictionary[tag].Enqueue(obj);
            obj = null;
            Pool pool = FindPoolByTag(tag);
            obj = Instantiate(pool.prefab);
            poolDictionary[tag].Enqueue(obj);
            obj.SetActive(false); 
        }
        else
        {
            poolDictionary[tag].Enqueue(obj);
        }
        obj.SetActive(true);
        return obj;
    }

    public Pool FindPoolByTag(string tag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                return pool;
            }
        }
        return null;
    }

    public void closeObj()
    {
        for(int i=0; i<this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
