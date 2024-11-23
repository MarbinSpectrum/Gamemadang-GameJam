using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] private UnitObj[] normalUnit;
    [SerializeField] private UnitObj otherUnit;
    [SerializeField] private int spawnCnt;

    public float[] xRange;
    public float[] yRange;

    
    public void Spawn()
    {
        for (int i = 0; i < spawnCnt; i++)
        {
            Vector3 pos = new Vector3(Random.Range(xRange[0], xRange[1]), Random.Range(yRange[0], yRange[1]));
            string name = normalUnit[Random.Range(0, normalUnit.Length)].gameObject.name;
            //UnitObj obj = ObjectPool.Instance.SpawnFromPool($"{name}");//노말 오브젝트 무작위로 호출
            UnitObj obj = ObjectPool.Instance.SpawnFromPool($"{normalUnit[0].gameObject.name}");//노말 오브젝트 무작위로 호출
            obj.SetUnit(i, (int)System.DateTime.Now.Ticks);
            obj.transform.position = pos;
            //Instantiate(unitObj, pos, Quaternion.identity);
        }
        for (int j = 0; j < 1; j++)
        {
            Vector3 pos = new Vector3(Random.Range(xRange[0], xRange[1]), Random.Range(yRange[0], yRange[1]));
            UnitObj obj = ObjectPool.Instance.SpawnFromPool($"{otherUnit.name}");
            obj.SetUnit(j, (int)System.DateTime.Now.Ticks);
            obj.transform.position = pos;
            //obj
        }
    }
}
