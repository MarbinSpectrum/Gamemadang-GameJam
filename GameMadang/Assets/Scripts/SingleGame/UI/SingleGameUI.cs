using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SingleGameUI : MonoBehaviour
{
    [SerializeField] private GameObject lifeUI;

    [SerializeField] private Button stageSelectBtn;
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button[] mainBtn;

    int life = 3;

    private void Awake()
    {
        GameManager.Instance.OnLife += UpdateLife;
        //씬 번호 확정되고 전달
        //stageSelectBtn.onClick.AddListener(() => SceneChange());
        //nextStageBtn.onClick.AddListener(() => SceneChange());
        //retryBtn.onClick.AddListener(() => SceneChange());
        //foreach(var btn in mainBtn)
        //    btn.onClick.AddListener(() => SceneChange());

    }
   

    private void SceneChange(int num)
    {
       SceneManager.LoadScene(num);
    }

    private void UpdateLife()
    {
        if (life == 0) return; //임시
        life--;
        lifeUI.transform.GetChild(life).gameObject.SetActive(false);
    }

    public void Test()
    {
        SceneManager.LoadScene(4);
    }
  
}
