using UnityEngine;
using System.Collections.Generic;
public class SpawnUnit : MonoBehaviour
{
    
    [SerializeField] private int normalSpawnCnt;

    [Header("NormalUnit")]
    [SerializeField] private UnitTag[] normalUnit;

    [Header("OtherUnit")]
    [SerializeField] private UnitTag otherUnit;

    [SerializeField] private float[] xRange;
    [SerializeField] private float[] yRange;

    
    public void Spawn()
    {
        if (normalUnit.Length > 0)
        {
            for (int i = 0; i < normalSpawnCnt; i++)
            {
                int randomIdx = Random.Range(0, normalUnit.Length);//·£´ý ³ë¸»À¯´Ö »ý¼º
                int seed = (int)System.DateTime.Now.Ticks;

                UnityEngine.Random.InitState(seed + i);
                float x = Random.Range(xRange[0], xRange[1]);
                UnityEngine.Random.InitState(seed + seed + i);
                float y = Random.Range(yRange[0], yRange[1]);
                Vector3 pos = new Vector3(x, y);

                UnitTag spawnUnitTag = normalUnit[randomIdx];
                UnitObj unitObj = ObjectPool.Instance.SpawnFromPool(spawnUnitTag);
                unitObj.SetUnit(i, (int)System.DateTime.Now.Ticks);
                unitObj.transform.position = pos;
            }
        }

        {
            int seed = (int)System.DateTime.Now.Ticks;

            UnityEngine.Random.InitState(seed);
            float x = Random.Range(xRange[0], xRange[1]);
            UnityEngine.Random.InitState(seed + seed);
            float y = Random.Range(yRange[0], yRange[1]);
            Vector3 pos = new Vector3(x, y);
            UnitObj unitObj = ObjectPool.Instance.SpawnFromPool(otherUnit);

            unitObj.SetUnit(20000, (int)System.DateTime.Now.Ticks);
            unitObj.transform.position = pos;
        }
    }

    public int GetSpawnCnt() => normalSpawnCnt;
    public UnitTag[] GetSpawnUnit() => normalUnit;
    public UnitTag GetSpawnOtherUnit() => otherUnit;
    public float[] GetSpawnxRange() => xRange;
    public float[] GetSpawnyRange() => yRange;
}
