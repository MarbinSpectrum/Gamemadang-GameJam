using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    
    [SerializeField] private int normalSpawnCnt;

    [Header("NormalUnit")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Material[] materials;

    [Header("OtherUnit")]
    [SerializeField] private Sprite otherSprite;
    [SerializeField] private Material otherMaterial;

    public float[] xRange;
    public float[] yRange;

    
    public void Spawn()
    {
        for (int i = 0; i < normalSpawnCnt; i++)
        {
            Vector3 pos = new Vector3(Random.Range(xRange[0], xRange[1]), Random.Range(yRange[0], yRange[1]));
            UnitObj unitObj = ObjectPool.Instance.SpawnFromPool("Obj1");

            int random = Random.Range(0, sprites.Length);//���� �븻���� ����

            SpriteRenderer sprite = unitObj.gameObject.GetComponent<SpriteRenderer>();//�ش� ������Ʈ�� ��Ƽ����, �̹����� �ٲ���
            sprite.material = materials[random];
            sprite.sprite = sprites[random];

            unitObj.SetUnit(i, (int)System.DateTime.Now.Ticks);
            unitObj.transform.position = pos;
        }
        for (int j = 0; j < 1; j++)
        {
            Vector3 pos = new Vector3(Random.Range(xRange[0], xRange[1]), Random.Range(yRange[0], yRange[1]));
            UnitObj unitObj = ObjectPool.Instance.SpawnFromPool("Obj2");

            SpriteRenderer sprite = unitObj.gameObject.GetComponent<SpriteRenderer>();//�ش� ������Ʈ�� ��Ƽ����, �̹����� �ٲ���
            sprite.material = otherMaterial;
            sprite.sprite = otherSprite;

            unitObj.SetUnit(j, (int)System.DateTime.Now.Ticks);
            unitObj.transform.position = pos;
        }
    }
}
