using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] private UnitObj unitObj;
    [SerializeField] private UnitObj findObj;
    [SerializeField] private int spawnCnt;

    public float[] xRange;
    public float[] yRange;

    private void Start()
    {
        Application.targetFrameRate = 120;

        for (int i = 0; i < spawnCnt; i++)
        {
            Vector3 pos = new Vector3(Random.Range(xRange[0], xRange[1]), Random.Range(yRange[0], yRange[1]));
            UnitObj obj = ObjectPool.Instance.SpawnFromPool("Obj1");
            obj.SetUnit(i, (int)System.DateTime.Now.Ticks);
            obj.transform.position = pos;
            //Instantiate(unitObj, pos, Quaternion.identity);
        }
        for(int j = 0;j<1;j++)
        {
            Vector3 pos = new Vector3(Random.Range(xRange[0], xRange[1]), Random.Range(yRange[0], yRange[1]));
            UnitObj obj = ObjectPool.Instance.SpawnFromPool("Obj2");
            obj.SetUnit(j, (int)System.DateTime.Now.Ticks);
            obj.transform.position = pos;
            //obj
        }
    }
}
