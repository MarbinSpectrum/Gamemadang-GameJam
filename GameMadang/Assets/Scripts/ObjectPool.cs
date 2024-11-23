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
    private Dictionary<string, List<UnitObj>> useList = new Dictionary<string, List<UnitObj>>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var pool in pools)
        {
            Queue<UnitObj> objectPool = new Queue<UnitObj>();

            for (int i = 0; i < pool.size; i++)
            {
                UnitObj obj = Instantiate(pool.prefabObj, this.transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

            List<UnitObj> objectList = new List<UnitObj>();
            useList.Add(pool.tag, objectList);
        }
    }

    public UnitObj SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        UnitObj obj = null;
        Queue<UnitObj> queue = poolDictionary[tag];
        if(queue.Count > 0)
            obj = poolDictionary[tag].Dequeue();
        else
        {
            Pool pool = FindPoolByTag(tag);
            obj = Instantiate(pool.prefabObj, transform);
        }
        useList[tag].Add(obj);
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
        foreach (string tag in useList.Keys)
        {
            List<UnitObj> units = useList[tag];
            for(int i = 0; i < units.Count; i++)
            {
                poolDictionary[tag].Enqueue(units[i]);
                units[i].gameObject.SetActive(false);
            }
            units.Clear();
        }
    }
}
