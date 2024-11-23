using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject[] maps;
    SpawnUnit spawnUnit;
    protected override void Awake()
    {
        base.Awake();
    }
    public void UnitSpawn()
    {
        spawnUnit.Spawn();
    }

    public GameObject GetMap(int stageNum)
    {
        spawnUnit= maps[stageNum - 1].transform.GetChild(0).GetComponent<SpawnUnit>();
        return maps[stageNum-1];
    }
}
