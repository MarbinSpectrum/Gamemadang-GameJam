using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public UnitObj prefabObj;
        public int size;

        public Pool(UnitObj pUnit, int pSize)
        {
            prefabObj = pUnit;
            size = pSize;
        }
    }

    private Dictionary<UnitTag, Pool> pools = new Dictionary<UnitTag, Pool>();
    private Dictionary<UnitTag, Queue<UnitObj>> poolDictionary = new Dictionary<UnitTag, Queue<UnitObj>>();
    private Dictionary<UnitTag, List<UnitObj>> useList = new Dictionary<UnitTag, List<UnitObj>>();

    protected override void Awake()
    {
        base.Awake();

        ObjResourceLoad();

        foreach (var poolPair in pools)
        {
            Queue<UnitObj> objectPool = new Queue<UnitObj>();

            for (int i = 0; i < poolPair.Value.size; i++)
            {
                UnitObj obj = Instantiate(poolPair.Value.prefabObj, this.transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(poolPair.Key, objectPool);

            List<UnitObj> objectList = new List<UnitObj>();
            useList.Add(poolPair.Key, objectList);
        }
    }

    private void ObjResourceLoad()
    {
        string loadFormat = "Unit/{0}";
        KeyValuePair<UnitTag, string>[] kvpArr = new KeyValuePair<UnitTag, string>[]
        {
            new KeyValuePair<UnitTag, string>(UnitTag.TestUnit_1,"TestUnit_1"),
            new KeyValuePair<UnitTag, string>(UnitTag.TestUnit_2,"TestUnit_2"),

            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_1,"Stage1-1"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_2,"Stage1-2"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_3,"Stage1-3"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_4,"Stage1-4"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_5,"Stage1-5"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_6,"Stage1-6"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_7,"Stage1-7"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage1_Other,"Stage1 Other"),

            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_1,"Stage2-1"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_2,"Stage2-2"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_3,"Stage2-3"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_4,"Stage2-4"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_5,"Stage2-5"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_6,"Stage2-6"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_7,"Stage2-7"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_8,"Stage2-8"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_9,"Stage2-9"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_10,"Stage2-10"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_11,"Stage2-11"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_12,"Stage2-12"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage2_Other,"Stage2 Other"),

            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_1,"Stage3-1"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_2,"Stage3-2"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_3,"Stage3-3"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_4,"Stage3-4"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_5,"Stage3-5"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_6,"Stage3-6"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_7,"Stage3-7"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_8,"Stage3-8"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_9,"Stage3-9"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_10,"Stage3-10"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_11,"Stage3-11"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_12,"Stage3-12"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_13,"Stage3-13"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage3_Other,"Stage3 Other"),

            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_1,"Stage4-1"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_2,"Stage4-2"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_3,"Stage4-3"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_4,"Stage4-4"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_5,"Stage4-5"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_6,"Stage4-6"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_7,"Stage4-7"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_8,"Stage4-8"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_9,"Stage4-9"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage4_Other,"Stage4 Other"),

            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_1,"Stage5-1"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_2,"Stage5-2"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_3,"Stage5-3"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_4,"Stage5-4"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_5,"Stage5-5"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_6,"Stage5-6"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_7,"Stage5-7"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_8,"Stage5-8"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_9,"Stage5-9"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_10,"Stage5-10"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage5_Other,"Stage5 Other"),

            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_1,"Stage6-1"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_2,"Stage6-2"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_3,"Stage6-3"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_4,"Stage6-4"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_5,"Stage6-5"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_6,"Stage6-6"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_7,"Stage6-7"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_8,"Stage6-8"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_9,"Stage6-9"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_10,"Stage6-10"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_11,"Stage6-11"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_12,"Stage6-12"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_13,"Stage6-13"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_14,"Stage6-14"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_15,"Stage6-15"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_16,"Stage6-16"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_17,"Stage6-17"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_18,"Stage6-18"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_19,"Stage6-19"),
            new KeyValuePair<UnitTag, string>(UnitTag.Stage6_Other,"Stage6 Other"),
        };

        foreach(KeyValuePair<UnitTag, string> pair in kvpArr)
        {
            string route = string.Format(loadFormat, pair.Value);
            UnitObj unitObj = Resources.Load<UnitObj>(route);
            pools.Add(pair.Key, new Pool(unitObj, 20));
        }

    }

    public UnitObj SpawnFromPool(UnitTag tag)
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

    public Pool FindPoolByTag(UnitTag tag)
    {
        if (pools.ContainsKey(tag))
            return pools[tag];
        return null;
    }

    public void ClearObj()
    {
        foreach (UnitTag tag in useList.Keys)
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
