using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject[] maps;
    protected override void Awake()
    {
        base.Awake();
    }

    public GameObject GetMap(int stageNum)
    {
        return maps[stageNum-1];
    }
}
