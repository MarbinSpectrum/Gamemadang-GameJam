using UnityEngine;

public class StageObj : MonoBehaviour
{
    [SerializeField] private SpawnUnit spawnUnit;

    public SpawnUnit GetSpawnUnit() => spawnUnit;
}
