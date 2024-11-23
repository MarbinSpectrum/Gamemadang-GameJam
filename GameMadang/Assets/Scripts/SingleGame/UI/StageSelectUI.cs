using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    public int stageCount;
    public Transform stageTransform;
    private int curOpenStage;//열린 스테이지
    [SerializeField] private GameObject prefab;
    private GameObject[] stages ;

    [SerializeField] private Button backBtn;

    private void Start()
    {
        backBtn.onClick.AddListener(() => SceneChange("Title"));
        stages = new GameObject[stageCount];
        curOpenStage = GameManager.Instance.ClearStage;
        Init();
    }

    private void Init()
    {
        for(int i=0; i<stageCount;i++)
        {
            GameObject obj=Instantiate(prefab, stageTransform);
            stages[i] = obj;

            if (obj.TryGetComponent<Button>(out Button btn))
            {
                btn.onClick.AddListener(() => SceneChange("SingleGame"));
                btn.gameObject.GetComponent<StageBtn>().num = i + 1;
                TextMeshProUGUI text = btn.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                text.text = $"{i + 1}";

                if (i < curOpenStage) OpenStage(obj);
            }
        }
    }
    private void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OpenStage(GameObject obj)
    {
        if (obj.TryGetComponent<Button>(out Button btn))
        {
            btn.interactable = true;
            obj.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    
  

}
