using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SingleGameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] lifeUI;

    [SerializeField] private Button stageSelectBtn;
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button[] mainBtn;

    [Header("ResultPanel")]
    [SerializeField] private GameObject GameClearPanel;
    [SerializeField] private GameObject GameOverPanel;

    [Header("CountDown")]
    [SerializeField] private GameObject countDownPanel;
    [SerializeField] private TextMeshProUGUI countDownText;

    [Header("CountDown")]
    [SerializeField] private TimeUI time;

    int life=3;

    private void Awake()
    {
        GameManager.Instance.OnLife += DecreaseLife;
        GameManager.Instance.OnScore += ClearStage;
        //씬 번호 확정되고 전달
        stageSelectBtn.onClick.AddListener(() => SceneChange("StageSelect"));
        nextStageBtn.onClick.AddListener(() => SceneChange("SingleGame"));
        retryBtn.onClick.AddListener(() => SceneChange("SingleGame"));
        foreach (var btn in mainBtn)
            btn.onClick.AddListener(() => SceneChange("Title"));

        countDownPanel.SetActive(true);
        GameManager.Instance.GameStart(countDownText);
    }

    private void SceneChange(string name)
    {
        ObjectPool.Instance.ClearObj();
        life = 3;
        GameManager.Instance.OnLife -= DecreaseLife;
        GameManager.Instance.OnScore -= ClearStage;
        SceneManager.LoadScene(name);
    }

    private void DecreaseLife()
    {
        life--;
        lifeUI[life].SetActive(false);

        if (life == 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }

    private void ClearStage()
    {
       
        Time.timeScale = 0;
        GameClearPanel.SetActive(true);
    }

}
