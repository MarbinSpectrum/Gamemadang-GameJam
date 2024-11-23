using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject[] maps;
    SpawnUnit spawnUnit;
    public GameObject curMap = null;
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
        if (curMap == null) UpdateMap();
        curMap.SetActive(true);
        UnitSpawn();
    }
    public void CloseMap()
    {
        curMap.SetActive(false);
        curMap = null;
    }
    public void UpdateMap()//�����ʲ��� ���ο� �ʹ޾ƿ���
    {
        if(curMap!=null)curMap.SetActive(false);
        curMap = GetMap(GameManager.Instance.curStage);
    }

    private GameObject GetMap(int stageNum)
    {
        spawnUnit= maps[stageNum - 1].transform.GetChild(0).GetComponent<SpawnUnit>();
        curMap = maps[stageNum - 1];

        return curMap;
    }
}
