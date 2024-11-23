using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] private UnitObj unitObj;
    [SerializeField] private UnitObj findObj;
    [SerializeField] private int spawnCnt;


    private void Start()
    {
        Application.targetFrameRate = 120;

        for (int i = 0; i < spawnCnt; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-3.0f, 3.0f));
            GameObject obj = ObjectPool.Instance.SpawnFromPool("Obj1");
            obj.transform.position = pos;
            //Instantiate(unitObj, pos, Quaternion.identity);
        }

        {
            Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-3.0f, 3.0f));
            Instantiate(findObj, pos, Quaternion.identity);
        }
    }
}
