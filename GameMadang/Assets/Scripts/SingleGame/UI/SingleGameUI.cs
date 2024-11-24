using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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

    [Header("Time")]
    [SerializeField] private TimeUI time;

    [Header("CutScene")]
    [SerializeField] private GameObject timeLine;
    [SerializeField] private SoundObj sound;


    private GameObject curMap=null;

    int life=3;

    private void Awake()
    {
        MapManager.Instance.CloseMap();
        ObjectPool.Instance.ClearObj();

        GameManager.Instance.OnLife = DecreaseLife;
        GameManager.Instance.OnScore = StartCutScene;
       
        stageSelectBtn.onClick.AddListener(() => SceneChange("StageSelect"));
        nextStageBtn.onClick.AddListener(() => SceneChange("SingleGame"));
        retryBtn.onClick.AddListener(() => SceneChange("SingleGame"));
        foreach (var btn in mainBtn)
            btn.onClick.AddListener(() => SceneChange("Title"));

        countDownPanel.SetActive(true);
        GameManager.Instance.GameStart(countDownText);

        MapManager.Instance.ActiveMap();
        
    }
    private void Update()
    {
        if(Time.timeScale!=0)
        {
            if (time.timeOver) GameOver();
        }
    }

    private void SceneChange(string name)
    {
        if(name== "SingleGame" )
        {
            if (GameManager.Instance.curStage > 6) name = "StageSelect";
        }
        SceneManager.LoadScene(name);
    }

    private void DecreaseLife()
    {
        life--;
        lifeUI[life].SetActive(false);

        if (life == 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        sound.PlaySound();
        GameOverPanel.SetActive(true);
    }
    private void StartCutScene()
    {
        timeLine.SetActive(true);
    }
    public void ClearStage()
    {
       
        if(GameManager.Instance.CompareStage())//최신 스테이지를 진행했을때만 
        {
            GameManager.Instance.ClearStage++;
            GameManager.Instance.curStage++;
        }
        else
        {
            GameManager.Instance.curStage++;
        }
       
        SaveLoad.Instance.Save();
        sound.PlaySound();
        GameClearPanel.SetActive(true);
    }

}
