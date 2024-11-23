using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SingleGameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] lifeUI;

    [SerializeField] private Button stageSelectBtn;
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button[] mainBtn;

    int life = 3;

    private void Awake()
    {
        GameManager.Instance.OnLife += UpdateLife;
        //�� ��ȣ Ȯ���ǰ� ����
        stageSelectBtn.onClick.AddListener(() => SceneChange("StageSelect"));
        nextStageBtn.onClick.AddListener(() => SceneChange("SigleGame"));
        retryBtn.onClick.AddListener(() => SceneChange("SigleGame"));
        foreach (var btn in mainBtn)
            btn.onClick.AddListener(() => SceneChange("Title"));

    }
   

    private void SceneChange(string name)
    {
        ObjectPool.Instance.ClearObj();
       SceneManager.LoadScene(name);
    }

    private void UpdateLife()
    {
        if (life == 0) return; //�ӽ�
        life--;
        lifeUI[life].SetActive(false);
    }

}
