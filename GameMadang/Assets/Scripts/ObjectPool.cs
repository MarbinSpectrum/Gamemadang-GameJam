using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public UnitObj prefabObj;
        public int size;
    }

    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<UnitObj>> poolDictionary = new Dictionary<string, Queue<UnitObj>>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var pool in pools)
        {
            Queue<UnitObj> objectPool = new Queue<UnitObj>();
            for (int i = 0; i < pool.size; i++)
            {
                // Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-3.0f, 3.0f));
                UnitObj obj = Instantiate(pool.prefabObj, this.transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public UnitObj SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        if (!poolDictionary[tag].TryDequeue(out UnitObj obj))
        {
            Pool pool = FindPoolByTag(tag);
            obj = Instantiate(pool.prefabObj, transform);
            poolDictionary[tag].Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
        else if (obj.gameObject.activeInHierarchy)
        {
            poolDictionary[tag].Enqueue(obj);
            obj = null;
            Pool pool = FindPoolByTag(tag);
            obj = Instantiate(pool.prefabObj, transform);
            poolDictionary[tag].Enqueue(obj);
            obj.gameObject.SetActive(false); 
        }
        else
        {
            poolDictionary[tag].Enqueue(obj);
        }
        obj.gameObject.SetActive(true);
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

    public void ClearObj()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
