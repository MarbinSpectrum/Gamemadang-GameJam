using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public int stageCount;
    private int curOpenStage;
    [SerializeField] private GameObject prefab;

    private GameObject[] stages ;

    private void Start()
    {
        stages = new GameObject[stageCount];
        curOpenStage = GameManager.Instance.ClearStage;
        Init();
    }

    private void Init()
    {
        for(int i=0; i<stageCount;i++)
        {
            GameObject obj=Instantiate(prefab, this.transform);
            stages[i] = obj;

            if (obj.TryGetComponent<Button>(out Button btn))
            {
                TextMeshProUGUI text = btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                text.text = $"{i + 1}";

                if (i < curOpenStage) OpenStage(obj);
            }
        }
    }

    private void OpenStage(GameObject obj)
    {
        if (obj.TryGetComponent<Button>(out Button btn))
        {
            btn.interactable = true;
            obj.transform.GetChild(1).gameObject.SetActive(false);

        }
    }
  

}
