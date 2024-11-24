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

    public void ActiveMap()//�� Ȱ��ȭ ���� ����
    {
        if (curMap == null) 
            UpdateMap();
        curMap.gameObject.SetActive(true);
        UnitSpawn();
    }

    public void CloseMap()
    {
        if(curMap != null)
        {
            curMap.gameObject.SetActive(false);
            curMap = null;
        }
       
    }

    public void UpdateMap()//�����ʲ��� ���ο� �ʹ޾ƿ���
        => UpdateMap(GameManager.Instance.curStage);

    public void UpdateMap(int stageNum)
    {
        if (curMap != null)
            curMap.gameObject.SetActive(false);
        curMap = GetMap(stageNum);
    }
    public StageObj GetMap(int stageNum)
    {
        spawnUnit = mapData[stageNum - 1].GetSpawnUnit();
        curMap = mapData[stageNum - 1];

        return curMap;
    }
}
