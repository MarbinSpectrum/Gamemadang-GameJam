using UnityEngine;
using System.Collections.Generic;

public class MultiSpawn : MonoBehaviour
{
    [SerializeField] private List<MultiUnit> multiUnit;
    private MultiUnit targetUnit;

    public void CreateUnit(int stageNum, int seed)
    {
        Application.targetFrameRate = 120;

        ObjectPool.Instance.ClearObj();

        MapManager.Instance.UpdateMap(stageNum + 1);
        MapManager.Instance.curMap.gameObject.SetActive(true);
        SpawnUnit spawnUnit = MapManager.Instance.curMap.GetSpawnUnit();

        int spawnCnt = spawnUnit.GetSpawnCnt();
        UnitTag[] normalUnit = spawnUnit.GetSpawnUnit();
        UnitTag otherUnit = spawnUnit.GetSpawnOtherUnit();
        float[] xRange = spawnUnit.GetSpawnxRange();
        float[] yRange = spawnUnit.GetSpawnyRange();

        if (normalUnit.Length > 0)
        {
            for (int i = 0; i < spawnCnt; i++)
            {
                UnityEngine.Random.InitState(seed + i);
                float x = Random.Range(xRange[0], xRange[1]);
                UnityEngine.Random.InitState(seed + seed + i);
                float y = Random.Range(yRange[0], yRange[1]);
                Vector3 pos = new Vector3(x, y);
                UnityEngine.Random.InitState(seed + seed + seed + i);
                int randomIdx = Random.Range(0, normalUnit.Length);//·£´ý ³ë¸»À¯´Ö »ý¼º
                UnitTag spawnUnitTag = normalUnit[randomIdx];
                UnitObj unitObj = ObjectPool.Instance.SpawnFromPool(spawnUnitTag);
                unitObj.SetUnit(i, (int)System.DateTime.Now.Ticks);
                unitObj.transform.position = pos;
            }
        }

        CreateMultiObj(seed, stageNum);
    }

    private void CreateMultiObj(int seed,int stageNum)
    {
        ClearMultiObj();

        MultiUnit unit = multiUnit[stageNum];
        unit.gameObject.SetActive(true);

        UnityEngine.Random.InitState(seed);
        float x = Random.Range(-7.0f, 7.0f);

        UnityEngine.Random.InitState(seed + seed);
        float y = Random.Range(-3.0f, 3.0f);

        Vector3 pos = new Vector3(x, y);
        unit.SetUnit(20000, seed);
        unit.transform.position = pos;

        unit.gameObject.SetActive(true);

        targetUnit = unit;
    }

    public void ClearMultiObj()
    {
        for (int i = 0; i < multiUnit.Count; i++)
            multiUnit[i].gameObject.SetActive(false);
    }

    public MultiUnit TargetUnit()
    {
        return targetUnit;
    }
}
