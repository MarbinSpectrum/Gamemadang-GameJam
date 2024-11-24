using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

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
        MapManager.Instance.CloseMap();
        ObjectPool.Instance.ClearObj();

        backBtn.onClick.AddListener(() => SceneChange("Title"));
        stages = new GameObject[stageCount];
        curOpenStage = GameManager.Instance.ClearStage;

        SaveLoad.Instance.Load();
        Init();
    }

    private void Init()
    {
        StartCoroutine(CreateButton());
    }
    IEnumerator CreateButton()
    {
        for (int i = 0; i < stageCount; i++)
        {
            GameObject obj = Instantiate(prefab, stageTransform);
            stages[i] = obj;

            if (obj.TryGetComponent<Button>(out Button btn))
            {
                btn.onClick.AddListener(() => SceneChange("SingleGame"));
                SoundObj sound = obj.AddComponent<SoundObj>();
                sound.sound = Sound.SE_Click;

                btn.onClick.AddListener(() => sound.PlaySound());
                btn.gameObject.GetComponent<StageBtn>().num = i + 1;
                TextMeshProUGUI text = btn.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                text.text = $"{i + 1}";

                if (i < curOpenStage) OpenStage(obj);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    private void SceneChange(string name)
    {
        DOTween.KillAll(); //버튼 두트윈 꺼주기
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
