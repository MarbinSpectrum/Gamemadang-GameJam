using TMPro;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for(int i=0; i<stageCount;i++)
        {
            GameObject obj=Instantiate(prefab,this.transform);

            if(obj.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
            {
                text.text = $"{i+1}";
            }
        }
    }

}
