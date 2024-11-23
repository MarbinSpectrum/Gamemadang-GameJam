using UnityEngine;

public class MultiSpawn : MonoBehaviour
{
    [SerializeField] private int spawnCnt;

    public void CreateUnit(int seed)
    {
        Application.targetFrameRate = 120;

        for (int i = 0; i < spawnCnt; i++)
        {
            UnityEngine.Random.InitState(seed + i);
            float x = Random.Range(-7.0f, 7.0f);

            UnityEngine.Random.InitState(seed + i);
            float y = Random.Range(-3.0f, 3.0f);

            Vector3 pos = new Vector3(x, y);
            UnitObj obj = ObjectPool.Instance.SpawnFromPool("Obj1");
            obj.SetUnit(i, seed);
            obj.transform.position = pos;
        }
    }
}
