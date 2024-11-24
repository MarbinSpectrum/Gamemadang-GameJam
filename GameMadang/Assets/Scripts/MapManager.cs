using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private StageObj[] mapData;
    SpawnUnit spawnUnit;
    public StageObj curMap
    {
        private set;
        get;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void UnitSpawn()
    {
        spawnUnit.Spawn();
    }

    public void ActiveMap()//¸Ê È°¼ºÈ­ À¯´Ö ½ºÆù
    {
        if (curMap == null) 
            UpdateMap();
        curMap.gameObject.SetActive(true);
        UnitSpawn();
    }

    public void CloseMap()
    {
        curMap.gameObject.SetActive(false);
        curMap = null;
    }

    public void UpdateMap()//±âÁ¸¸Ê²ô°í »õ·Î¿î ¸Ê¹Þ¾Æ¿À±â
    {
        if(curMap!=null)
            curMap.gameObject.SetActive(false);
        curMap = GetMap(GameManager.Instance.curStage);
    }

    private StageObj GetMap(int stageNum)
    {
        spawnUnit = mapData[stageNum - 1].GetSpawnUnit();
        curMap = mapData[stageNum - 1];

        return curMap;
    }
}
